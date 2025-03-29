using LibraryPatternsImpl.Books;

namespace LibraryPatternsImpl.Catalog;

public class Catalog
{
    private readonly Dictionary<int, BookStock> _books = [];

    public void AddBook(int bookId, int quantity = 1)
    {
        if (_books.TryGetValue(bookId, out var stock))
        {
            stock.Quantity += quantity;
        }
        else
        {
            throw new InvalidOperationException($"No book with id = {bookId}");
        }
    }

    public bool RemoveBook(int bookId, int quantity = 1)
    {
        if (!_books.TryGetValue(bookId, out var stock)) return false;
        stock.Quantity = stock.Quantity - quantity;

        if (stock.Quantity == 0)
        {
            _books.Remove(bookId);
        }
        else if (stock.Quantity < 0)
        {
            throw new InvalidOperationException($"Can't remove {quantity} books from {stock.Quantity} left!");
        }
        return true;
    }

    public int GetAvailableQuantity(int bookId)
    {
        return _books.TryGetValue(bookId, out var stock) ? stock.Quantity : 0;
    }

    public (IBook book, int quantity)? GetBookInfo(int bookId)
    {
        if (_books.TryGetValue(bookId, out var stock))
        {
            return (stock.Book, stock.Quantity);
        }
        return null;
    }

    public List<IBook> GetAllAvailableBooks()
    {
        return _books.Values
            .Where(s => s.Quantity > 0)
            .Select(s => s.Book)
            .ToList();
    }

    public List<(IBook book, int quantity)> FindBooks(Func<IBook, bool> predicate)
    {
        return _books.Values
            .Where(s => predicate(s.Book))
            .Select(s => (s.Book, s.Quantity))
            .ToList();
    }
}