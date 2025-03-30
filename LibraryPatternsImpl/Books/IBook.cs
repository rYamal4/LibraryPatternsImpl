using LibraryPatternsImpl.Books.State;
using LibraryPatternsImpl.Users;

namespace LibraryPatternsImpl.Books;

public interface IBook
{
    int Id { get; }
    string Title { get; }
    string Author { get; }
    int Year { get; }
    string Genre { get; }
    BookState State { get; set; }
    string GetInfo();
    bool Borrow(IUser user);
    bool Return(IUser user);
}