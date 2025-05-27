using LibraryPatternsImpl.Books.State;
using LibraryPatternsImpl.Users;

namespace LibraryPatternsImpl.Books;

class ReservedBook(IBook book) : AbstractBookDecorator(book)
{
    public override bool Borrow(IUser user)
    {
        if (user.IsLibrarian || _book.State is BorrowedState)
        {
            return _book.Borrow(user);
        }
        Console.WriteLine($"Sorry, but book '{Title}' is reserved.");
        return false;
    }

    public override bool Return(IUser user)
    {
        if (user.IsLibrarian || _book.State is BorrowedState)
        {
            return _book.Return(user);
        }
        Console.WriteLine($"Sorry, but book '{Title}' is reserved.");
        return false;
    }

    public override string GetInfo() =>
    $"[{Id}] {Title} by {Author} ({Year}) - {Genre} | State: {GetStateName()}";

    private string GetStateName() => _book.State is BorrowedState ? "Borrowed" : "Reserved";
}
