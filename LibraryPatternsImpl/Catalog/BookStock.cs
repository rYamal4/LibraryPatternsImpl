using LibraryPatternsImpl.Books;

namespace LibraryPatternsImpl.Catalog;

public class BookStock(IBook book, int quantity)
{
    public IBook Book { get; } = book;
    public int Quantity { get; set; } = quantity;
}