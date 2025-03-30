using LibraryPatternsImpl.Books;
using LibraryPatternsImpl.SearchQuery.SearchExpression;
using Moq;

namespace LibraryPatternsTest.SearchQuery;

[TestFixture]
public class ExpressionInterpreterTests
{
    private List<IBook> _books;

    [SetUp]
    public void SetUp()
    {
        var book1 = new Mock<IBook>();
        book1.SetupGet(b => b.Id).Returns(1);
        book1.SetupGet(b => b.Title).Returns("Dune");
        book1.SetupGet(b => b.Author).Returns("Frank Herbert");
        book1.SetupGet(b => b.Year).Returns(1965);
        book1.SetupGet(b => b.Genre).Returns("Sci-Fi");
        book1.Setup(b => b.GetInfo()).Returns("Dune by Frank Herbert, 1965 (Sci-Fi)");

        var book2 = new Mock<IBook>();
        book2.SetupGet(b => b.Id).Returns(2);
        book2.SetupGet(b => b.Title).Returns("Dune Messiah");
        book2.SetupGet(b => b.Author).Returns("Frank Herbert");
        book2.SetupGet(b => b.Year).Returns(1969);
        book2.SetupGet(b => b.Genre).Returns("Sci-Fi");
        book2.Setup(b => b.GetInfo()).Returns("Dune Messiah by Frank Herbert, 1969 (Sci-Fi)");

        var book3 = new Mock<IBook>();
        book3.SetupGet(b => b.Id).Returns(3);
        book3.SetupGet(b => b.Title).Returns("1984");
        book3.SetupGet(b => b.Author).Returns("George Orwell");
        book3.SetupGet(b => b.Year).Returns(1949);
        book3.SetupGet(b => b.Genre).Returns("Dystopia");
        book3.Setup(b => b.GetInfo()).Returns("1984 by George Orwell, 1949 (Dystopia)");

        var book4 = new Mock<IBook>();
        book4.SetupGet(b => b.Id).Returns(4);
        book4.SetupGet(b => b.Title).Returns("Foundation");
        book4.SetupGet(b => b.Author).Returns("Isaac Asimov");
        book4.SetupGet(b => b.Year).Returns(1951);
        book4.SetupGet(b => b.Genre).Returns("Sci-Fi");
        book4.Setup(b => b.GetInfo()).Returns("Foundation by Isaac Asimov, 1951 (Sci-Fi)");

        var book5 = new Mock<IBook>();
        book5.SetupGet(b => b.Id).Returns(5);
        book5.SetupGet(b => b.Title).Returns("Brave New World");
        book5.SetupGet(b => b.Author).Returns("Aldous Huxley");
        book5.SetupGet(b => b.Year).Returns(1932);
        book5.SetupGet(b => b.Genre).Returns("Dystopia");
        book5.Setup(b => b.GetInfo()).Returns("Brave New World by Aldous Huxley, 1932 (Dystopia)");

        var book6 = new Mock<IBook>();
        book6.SetupGet(b => b.Id).Returns(6);
        book6.SetupGet(b => b.Title).Returns("Neuromancer");
        book6.SetupGet(b => b.Author).Returns("William Gibson");
        book6.SetupGet(b => b.Year).Returns(1984);
        book6.SetupGet(b => b.Genre).Returns("Cyberpunk");
        book6.Setup(b => b.GetInfo()).Returns("Neuromancer by William Gibson, 1984 (Cyberpunk)");

        var book7 = new Mock<IBook>();
        book7.SetupGet(b => b.Id).Returns(7);
        book7.SetupGet(b => b.Title).Returns("Snow Crash");
        book7.SetupGet(b => b.Author).Returns("Neal Stephenson");
        book7.SetupGet(b => b.Year).Returns(1992);
        book7.SetupGet(b => b.Genre).Returns("Cyberpunk");
        book7.Setup(b => b.GetInfo()).Returns("Snow Crash by Neal Stephenson, 1992 (Cyberpunk)");

        var book8 = new Mock<IBook>();
        book8.SetupGet(b => b.Id).Returns(8);
        book8.SetupGet(b => b.Title).Returns("The Left Hand of Darkness");
        book8.SetupGet(b => b.Author).Returns("Ursula K. Le Guin");
        book8.SetupGet(b => b.Year).Returns(1969);
        book8.SetupGet(b => b.Genre).Returns("Sci-Fi");
        book8.Setup(b => b.GetInfo()).Returns("The Left Hand of Darkness by Ursula K. Le Guin, 1969 (Sci-Fi)");

        var book9 = new Mock<IBook>();
        book9.SetupGet(b => b.Id).Returns(9);
        book9.SetupGet(b => b.Title).Returns("Fahrenheit 451");
        book9.SetupGet(b => b.Author).Returns("Ray Bradbury");
        book9.SetupGet(b => b.Year).Returns(1953);
        book9.SetupGet(b => b.Genre).Returns("Dystopia");
        book9.Setup(b => b.GetInfo()).Returns("Fahrenheit 451 by Ray Bradbury, 1953 (Dystopia)");

        var book10 = new Mock<IBook>();
        book10.SetupGet(b => b.Id).Returns(10);
        book10.SetupGet(b => b.Title).Returns("Animal Farm");
        book10.SetupGet(b => b.Author).Returns("George Orwell");
        book10.SetupGet(b => b.Year).Returns(1945);
        book10.SetupGet(b => b.Genre).Returns("Political satire");
        book10.Setup(b => b.GetInfo()).Returns("Animal Farm by George Orwell, 1945 (Political satire)");

        _books = new List<IBook>
            {
                book1.Object,
                book2.Object,
                book3.Object,
                book4.Object,
                book5.Object,
                book6.Object,
                book7.Object,
                book8.Object,
                book9.Object,
                book10.Object
            };
    }

    [Test]
    public void Interpret_SimpleNumericQuery_ReturnsBooksMatchingYearCondition()
    {
        string query = "year>=1950";
        var interpreter = SearchExpressionInterpreter.Create(query);
        var context = new Context(_books);
        List<IBook> result = interpreter.Interpret(context);
        var expectedTitles = new List<string>
    {
        "Dune",
        "Dune Messiah",
        "Foundation",
        "Neuromancer",
        "Snow Crash",
        "The Left Hand of Darkness",
        "Fahrenheit 451"
    };
        CollectionAssert.AreEquivalent(expectedTitles, result.Select(b => b.Title));
    }

    [Test]
    public void Interpret_ComplexQuery_ReturnsMatchingBooks()
    {
        string query = "(title='Dune' Or year>=1965) And genre='Sci-Fi'";
        var interpreter = SearchExpressionInterpreter.Create(query);
        var context = new Context(_books);
        List<IBook> result = interpreter.Interpret(context);
        var expectedTitles = new List<string> { "Dune", "Dune Messiah", "The Left Hand of Darkness" };
        CollectionAssert.AreEquivalent(expectedTitles, result.Select(b => b.Title));
    }

    [Test]
    public void Interpret_NestedComplexQuery_ReturnsMatchingBooks()
    {
        string query = "((title='Dune' Or title='Foundation') And year>=1950) Or (genre='Dystopia' And year<1950)";
        var interpreter = SearchExpressionInterpreter.Create(query);
        var context = new Context(_books);
        List<IBook> result = interpreter.Interpret(context);
        var expectedTitles = new List<string> { "Dune", "Dune Messiah", "Foundation", "Brave New World", "1984" };
        CollectionAssert.AreEquivalent(expectedTitles, result.Select(b => b.Title));
    }

    [Test]
    public void Interpret_InvalidOperatorForString_ThrowsQuerySyntaxException()
    {
        string query = "title>'Dune'";
        Assert.Throws<QuerySyntaxException>(() => SearchExpressionInterpreter.Create(query));
    }

    [Test]
    public void Interpret_InvalidNumberFormat_ThrowsQuerySyntaxException()
    {
        string query = "year>='notanumber'";
        var context = new Context(_books);
        Assert.Throws<QuerySyntaxException>(() =>
        {
            var interpreter = SearchExpressionInterpreter.Create(query);
            interpreter.Interpret(context);
        });
    }
}