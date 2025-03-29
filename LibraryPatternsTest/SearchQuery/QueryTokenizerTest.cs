using LibraryPatternsImpl.SearchQuery;

namespace LibraryPatternsTest.SearchQuery;

[TestFixture]
public class TokenizerTests
{
    private Tokenizer _tokenizer;

    [SetUp]
    public void Setup() => _tokenizer = new Tokenizer();

    private string FormatTokens(List<Token> tokens)
    {
        return string.Join(", ", tokens.Select(t => $"{t.type}: '{t.value}'"));
    }

    [Test]
    public void Tokenize_SimpleCondition_ReturnsCorrectTokens()
    {
        var input = "title='Dune'";
        var expected = "Title: 'title', Equal: '=', StringLiteral: 'Dune'";
        var result = FormatTokens(_tokenizer.Tokenize(input));
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Tokenize_ComplexQuery_ReturnsCorrectTokens()
    {
        var input = "(author='Herbert' And year>=1965) Or genre='Sci-Fi'";
        var expected = "LeftParen: '(', Author: 'author', Equal: '=', StringLiteral: 'Herbert', "
                     + "And: 'And', Year: 'year', MoreOrEqual: '>=', NumberLiteral: '1965', "
                     + "RightParen: ')', Or: 'Or', Genre: 'genre', Equal: '=', StringLiteral: 'Sci-Fi'";

        var tokens = _tokenizer.Tokenize(input);
        Assert.That(FormatTokens(tokens), Is.EqualTo(expected));
    }

    [Test]
    public void Tokenize_NestedConditions_ReturnsCorrectTokens()
    {
        var input = "((title='Foundation' And year<1950) Or (author='Asimov'))";
        var expected = "LeftParen: '(', LeftParen: '(', Title: 'title', Equal: '=', "
                     + "StringLiteral: 'Foundation', And: 'And', Year: 'year', Less: '<', "
                     + "NumberLiteral: '1950', RightParen: ')', Or: 'Or', LeftParen: '(', "
                     + "Author: 'author', Equal: '=', StringLiteral: 'Asimov', RightParen: ')', "
                     + "RightParen: ')'";

        var tokens = _tokenizer.Tokenize(input);
        Assert.That(FormatTokens(tokens), Is.EqualTo(expected));
    }

    [Test]
    public void Tokenize_MixedOperators_ReturnsCorrectTokens()
    {
        var input = "year>2000 And genre='Fiction' Or author='Tolstoy'";
        var expected = "Year: 'year', More: '>', NumberLiteral: '2000', And: 'And', "
                     + "Genre: 'genre', Equal: '=', StringLiteral: 'Fiction', Or: 'Or', "
                     + "Author: 'author', Equal: '=', StringLiteral: 'Tolstoy'";

        var tokens = _tokenizer.Tokenize(input);
        Assert.That(FormatTokens(tokens), Is.EqualTo(expected));
    }

    [Test]
    public void Tokenize_WithNumberLiterals_ReturnsCorrectTokens()
    {
        var input = "year=2023 Or year<2000";
        var expected = "Year: 'year', Equal: '=', NumberLiteral: '2023', Or: 'Or', "
                     + "Year: 'year', Less: '<', NumberLiteral: '2000'";

        var tokens = _tokenizer.Tokenize(input);
        Assert.That(FormatTokens(tokens), Is.EqualTo(expected));
    }

    [Test]
    public void Tokenize_InvalidInput_ThrowsException()
    {
        var input = "title#='Invalid'";
        Assert.Throws<InvalidOperationException>(() => _tokenizer.Tokenize(input));
    }

    [Test]
    public void Tokenize_UnclosedString_ThrowsException()
    {
        var input = "author='Unclosed";
        Assert.Throws<InvalidOperationException>(() => _tokenizer.Tokenize(input));
    }
}