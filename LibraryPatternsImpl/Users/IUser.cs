using LibraryPatternsImpl.Books;

namespace LibraryPatternsImpl.Users;

public interface IUser
{
    List<IBook> BorrowedBooks { get; }
    string FirstName { get; }
    string LastName { get; }
    bool IsLibrarian { get; }

    void BorrowBook(IBook book);
    void PrintBorrowedBooks();
    bool ReturnBook(int bookId);

}