using LibraryPatternsImpl.Books;

namespace LibraryPatternsImpl.Users;

public class LibraryUser(string firstName, string lastName) : IUser, ISubscriber
{
    public string FirstName { get; } = firstName;
    public string LastName { get; } = lastName;
    public List<IBook> BorrowedBooks { get; } = [];
    public List<string> Notifications { get; } = [];

    public void BorrowBook(IBook book) => BorrowedBooks.Add(book);

    public bool ReturnBook(int bookId)
    {
        var book = BorrowedBooks.FirstOrDefault(b => b.Id == bookId);
        if (book == null) return false;
        BorrowedBooks.Remove(book);
        return true;
    }

    public void PrintBorrowedBooks()
    {
        if (BorrowedBooks.Count == 0)
        {
            Console.WriteLine("No borrowed books.");
            return;
        }

        Console.WriteLine("Your borrowed books:");
        foreach (var book in BorrowedBooks)
            Console.WriteLine(book.GetInfo());
    }

    public void ShowNotifications()
    {
        if (Notifications.Count == 0) return;

        Console.WriteLine("\n--- Notifications ---");
        foreach (var message in Notifications)
            Console.WriteLine(message);
        Notifications.Clear();
    }

    public void Update(string message)
    {
        Notifications.Add(message);
    }
}