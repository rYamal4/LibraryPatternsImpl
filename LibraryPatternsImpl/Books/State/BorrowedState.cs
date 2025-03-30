using LibraryPatternsImpl.Users;

namespace LibraryPatternsImpl.Books.State;

public class BorrowedState(IBook book) : BookState(book)
{
    public override bool Borrow(IUser user)
    {
        Console.WriteLine($"Book '{Book.Title}' is already borrowed.");
        return false;
    }

    public override bool Return(IUser user)
    {
        if (!user.BorrowedBooks.Any(b => b.Id == Book.Id))
        {
            Console.WriteLine($"Book '{Book.Title}' is not borrowed by you.");
            return false;
        }
        Book.State = new AvailableState(Book);
        user.ReturnBook(Book.Id);
        Console.WriteLine($"Book '{Book.Title}' has been returned.");
        return true;
    }

    public override string GetStateName() => "Borrowed";
}