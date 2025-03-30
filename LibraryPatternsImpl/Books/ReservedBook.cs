using LibraryPatternsImpl.Users;

namespace LibraryPatternsImpl.Books;

class ReservedBook(IBook book) : AbstractBookDecorator(book)
{
    public override bool Borrow(IUser user)
    {
        Console.WriteLine($"Sorry, but book '{Title}' is reserved.");
        return false;
    }

    public override bool Return(IUser user)
    {
        Console.WriteLine($"Sorry, but book '{Title}' is reserved.");
        return false;
    }

    public override string GetInfo() =>
    $"[{Id}] {Title} by {Author} ({Year}) - {Genre} | State: Reserved";
}
