using LibraryPatternsImpl.Books;

namespace LibraryPatternsImpl.Catalog;

public interface ICatalog
{
    void AddBook(IBook book);
    bool RemoveBook(int bookId);
    List<IBook> FindBooks(string searchQuery);
    List<IBook> GetAllBooks();
    IBook? GetBookById(int bookId);
}