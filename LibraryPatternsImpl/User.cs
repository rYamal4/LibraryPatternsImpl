using LibraryPatternsImpl.Books;

namespace LibraryPatternsImpl;

public class User(string firstName, string lastName)
{
    public string FirstName { get; } = firstName;
    public string LastName { get; } = lastName;
    public List<IBook> BorrowedBooks { get; } = [];

    public void Update(string message)
    {
        Console.WriteLine($"Уведомление для {FirstName} {LastName}: {message}");
    }

    public void BorrowBook(IBook book)
    {
        ArgumentNullException.ThrowIfNull(book);

        BorrowedBooks.Add(book);
        Console.WriteLine($"{FirstName} взял книгу: {book.Title}");
    }

    public bool ReturnBook(int bookId)
    {
        var book = BorrowedBooks.FirstOrDefault(b => b.Id == bookId);
        if (book != null)
        {
            BorrowedBooks.Remove(book);
            Console.WriteLine($"{FirstName} вернул книгу: {book.Title}");
            return true;
        }
        return false;
    }

    public void PrintBorrowedBooks()
    {
        Console.WriteLine($"{FirstName} {LastName}'s borrowed books:");
        foreach (var book in BorrowedBooks)
        {
            Console.WriteLine($"- {book.GetInfo()}");
        }
    }
}
