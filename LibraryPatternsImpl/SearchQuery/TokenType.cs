namespace LibraryPatternsImpl.SearchQuery;

public enum TokenType
{
    Title,
    Author,
    Year,
    Genre,
    And,
    Or,
    LeftParen,
    RightParen,
    Equal,
    More,
    Less,
    MoreOrEqual,
    LessOrEqual,
    NumberLiteral,
    StringLiteral,
}