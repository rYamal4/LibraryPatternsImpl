using LibraryPatternsImpl.Books;

namespace LibraryPatternsImpl.SearchQuery.SearchExpression;

public class SearchExpressionInterpreter : AbstractExpression
{
    private readonly AbstractExpression _ast;

    private SearchExpressionInterpreter(AbstractExpression ast)
    {
        _ast = ast ?? throw new ArgumentNullException(nameof(ast));
    }

    public static SearchExpressionInterpreter Create(string query)
    {
        var tokenizer = new Tokenizer();
        List<Token> tokens = tokenizer.Tokenize(query);
        AbstractExpression ast = ParseTokens(tokens);
        return new SearchExpressionInterpreter(ast);
    }

    public override List<IBook> Interpret(Context context)
    {
        return _ast.Interpret(context);
    }

    private static AbstractExpression ParseTokens(List<Token> tokens)
    {
        int currentIndex = 0;
        return ParseExpression(tokens, ref currentIndex);
    }

    private static AbstractExpression ParseExpression(List<Token> tokens, ref int currentIndex)
    {
        AbstractExpression left = ParseTerm(tokens, ref currentIndex);
        while (currentIndex < tokens.Count && tokens[currentIndex].type == TokenType.Or)
        {
            currentIndex++;
            AbstractExpression right = ParseTerm(tokens, ref currentIndex);
            left = new OrExpression(left, right);
        }
        return left;
    }

    private static AbstractExpression ParseTerm(List<Token> tokens, ref int currentIndex)
    {
        AbstractExpression left = ParseFactor(tokens, ref currentIndex);
        while (currentIndex < tokens.Count && tokens[currentIndex].type == TokenType.And)
        {
            currentIndex++;
            AbstractExpression right = ParseFactor(tokens, ref currentIndex);
            left = new AndExpression(left, right);
        }
        return left;
    }

    private static AbstractExpression ParseFactor(List<Token> tokens, ref int currentIndex)
    {
        if (currentIndex < tokens.Count && tokens[currentIndex].type == TokenType.LeftParen)
        {
            currentIndex++;
            AbstractExpression expr = ParseExpression(tokens, ref currentIndex);
            Consume(tokens, ref currentIndex, TokenType.RightParen);
            return expr;
        }
        return ParseCondition(tokens, ref currentIndex);
    }

    private static AbstractExpression ParseCondition(List<Token> tokens, ref int currentIndex)
    {
        Token fieldToken = Consume(tokens, ref currentIndex,
            TokenType.Title, TokenType.Author, TokenType.Year, TokenType.Genre);

        Token opToken = Consume(tokens, ref currentIndex,
            TokenType.Equal, TokenType.More, TokenType.Less,
            TokenType.MoreOrEqual, TokenType.LessOrEqual);

        Token valueToken = Consume(tokens, ref currentIndex,
            TokenType.NumberLiteral, TokenType.StringLiteral);

        return CreateCondition(fieldToken, opToken, valueToken);
    }

    private static AbstractExpression CreateCondition(Token field, Token op, Token value)
    {
        Func<IBook, bool> predicate = field.type switch
        {
            TokenType.Title => CreateStringPredicate(b => b.Title, op, value),
            TokenType.Author => CreateStringPredicate(b => b.Author, op, value),
            TokenType.Genre => CreateStringPredicate(b => b.Genre, op, value),
            TokenType.Year => CreateNumericPredicate(b => b.Year, op, value),
            _ => throw new QuerySyntaxException($"Unsupported field: {field.type}")
        };

        return new TerminalExpression(predicate);
    }

    private static Func<IBook, bool> CreateStringPredicate(
        Func<IBook, string> fieldSelector, Token op, Token value)
    {
        string expected = value.value;
        return op.type switch
        {
            TokenType.Equal => b => fieldSelector(b)?.Contains(expected, StringComparison.OrdinalIgnoreCase) == true,
            _ => throw new QuerySyntaxException($"Unsupported operator {op.type} for string comparison")
        };
    }

    private static Func<IBook, bool> CreateNumericPredicate(
        Func<IBook, int> fieldSelector, Token op, Token value)
    {
        if (!int.TryParse(value.value, out int numericValue))
            throw new QuerySyntaxException($"Invalid number format: {value.value}");

        return op.type switch
        {
            TokenType.Equal => b => fieldSelector(b) == numericValue,
            TokenType.More => b => fieldSelector(b) > numericValue,
            TokenType.Less => b => fieldSelector(b) < numericValue,
            TokenType.MoreOrEqual => b => fieldSelector(b) >= numericValue,
            TokenType.LessOrEqual => b => fieldSelector(b) <= numericValue,
            _ => throw new QuerySyntaxException($"Unsupported operator {op.type} for numeric comparison")
        };
    }

    private static Token Consume(List<Token> tokens, ref int currentIndex, params TokenType[] expectedTypes)
    {
        if (currentIndex >= tokens.Count)
            throw new QuerySyntaxException("Unexpected end of query");

        Token token = tokens[currentIndex];
        if (!expectedTypes.Contains(token.type))
            throw new QuerySyntaxException($"Expected {string.Join(" or ", expectedTypes)} at position {currentIndex}");

        currentIndex++;
        return token;
    }
}