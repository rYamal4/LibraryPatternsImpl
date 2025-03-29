namespace LibraryPatternsImpl.SearchQuery;

public class Tokenizer
{
    public List<Token> Tokenize(string query)
    {
        List<Token> tokens = [];
        for (int i = 0; i < query.Length; i++)
        {
            Token? token = null;
            int tokenEndPos = i;
            bool ws = false;
            for (int j = 1; j < query.Length - i + 1; j++)
            {
                var test = query[i + j - 1];
                if (query[i + j - 1].Equals(' '))
                {
                    ws = true;
                    break;
                }
                string sub = query.Substring(i, j);
                Token? replace = Token.Create(sub);
                if (replace != null)
                {
                    token = replace;
                    tokenEndPos = i + j - 1;
                }
                if (token != null && token.type == TokenType.StringLiteral)
                {
                    break;
                }
            }
            if (token != null)
            {
                tokens.Add(token);
            }
            if (token == null && !ws)
            {
                var test = query[i..^0];
                throw new InvalidOperationException("Could not tokenize query");
            }
            i = tokenEndPos;
        }
        return tokens;
    }
}
