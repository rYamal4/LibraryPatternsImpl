using System.Text.RegularExpressions;

namespace LibraryPatternsImpl.SearchQuery;

public record Token(TokenType type, string value)
{
    public readonly TokenType type = type;
    public readonly string value = value;

    public static Token Create(string str)
    {
        if (Match(str, @"^>=$")) return new Token(TokenType.MoreOrEqual, str);
        if (Match(str, @"^<=$")) return new Token(TokenType.LessOrEqual, str);
        if (Match(str, @"^=$")) return new Token(TokenType.Equal, str);
        if (Match(str, @"^>$")) return new Token(TokenType.More, str);
        if (Match(str, @"^<$")) return new Token(TokenType.Less, str);
        if (Match(str, @"^\($")) return new Token(TokenType.LeftParen, str);
        if (Match(str, @"^\)$")) return new Token(TokenType.RightParen, str);
        if (Match(str, @"^title$")) return new Token(TokenType.Title, str);
        if (Match(str, @"^author$")) return new Token(TokenType.Author, str);
        if (Match(str, @"^year$")) return new Token(TokenType.Year, str);
        if (Match(str, @"^genre$")) return new Token(TokenType.Genre, str);
        if (Match(str, @"^and$")) return new Token(TokenType.And, str);
        if (Match(str, @"^or$")) return new Token(TokenType.Or, str);
        if (Match(str, @"^[-+]?\d+$")) return new Token(TokenType.NumberLiteral, str);
        if (Match(str, @"^'(?:[^'\\]|\\.)*'"))
        {
            var content = str.Trim('\'');
            return new Token(TokenType.StringLiteral, content);
        }
        return null;
    }

    private static bool Match(string str, string pattern)
    {
        return Regex.IsMatch(str, pattern, RegexOptions.IgnoreCase);
    }
}