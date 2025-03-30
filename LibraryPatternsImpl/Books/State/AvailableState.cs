using LibraryPatternsImpl.Users;

namespace LibraryPatternsImpl.Books.State;

public class AvailableState(IBook book) : BookState(book)
{
    public override bool Borrow(IUser user)
    {
        Book.State = new BorrowedState(Book);
        user.BorrowBook(Book);
        Console.WriteLine($"Book '{Book.Title}' has been borrowed.");
        return true;
    }

    public override bool Return(IUser user)
    {
        Console.WriteLine($"Book '{Book.Title}' is already available.");
        return false;
    }

    public override string GetStateName() => "Available";
}