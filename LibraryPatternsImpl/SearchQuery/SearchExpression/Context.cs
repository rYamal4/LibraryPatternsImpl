using LibraryPatternsImpl.Books;

namespace LibraryPatternsImpl.SearchQuery.SearchExpression;

public class Context(List<IBook> books)
{
    public List<IBook> Books { get; } = books;
}