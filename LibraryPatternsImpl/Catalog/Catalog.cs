using LibraryPatternsImpl.Books;
using LibraryPatternsImpl.SearchQuery.SearchExpression;

namespace LibraryPatternsImpl.Catalog;

public class Catalog : ICatalog
{
    private readonly Dictionary<int, IBook> _books = new();

    public void AddBook(IBook book)
    {
        if (!_books.ContainsKey(book.Id))
            _books[book.Id] = book;
    }

    public bool RemoveBook(int bookId)
    {
        return _books.Remove(bookId);
    }

    public List<IBook> FindBooks(string searchQuery)
    {
        var interpreter = SearchExpressionInterpreter.Create(searchQuery);
        var context = new Context(_books.Values.ToList());
        return interpreter.Interpret(context);
    }

    public List<IBook> GetAllBooks() => _books.Values.ToList();

    public IBook? GetBookById(int bookId) =>
        _books.TryGetValue(bookId, out var book) ? book : null;
}