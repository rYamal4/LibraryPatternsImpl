using LibraryPatternsImpl.Books;
using LibraryPatternsImpl.Books.State;
using LibraryPatternsImpl.Catalog;
using LibraryPatternsImpl.Users;

namespace LibraryPatternsImpl.CLI;

public class LibraryFacade(ICatalog catalog, IUser user, BookNotifier notifier)
{
    private readonly ICatalog _catalog = catalog;
    private readonly IUser _user = user;
    private readonly BookNotifier _notifier = notifier;

    public void ShowMenu()
    {
        while (true)
        {
            Console.WriteLine("\n--- Library Menu ---");
            Console.WriteLine("1. View all books");
            Console.WriteLine("2. Search books");
            Console.WriteLine("3. Borrow book");
            Console.WriteLine("4. Return book");
            Console.WriteLine("5. View borrowed books");
            Console.WriteLine("6. Add book to wishlist");
            Console.WriteLine("0. Log out");
            Console.Write("Choose option: ");

            string? input = Console.ReadLine();
            switch (input)
            {
                case "1": DisplayAllBooks(); break;
                case "2": SearchBooks(); break;
                case "3": BorrowBook(); break;
                case "4": ReturnBook(); break;
                case "5": _user.PrintBorrowedBooks(); break;
                case "6": AddToWishlist(); break;
                case "0":
                    Console.WriteLine("Logging out...");
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    private void DisplayAllBooks()
    {
        var books = _catalog.GetAllBooks();
        if (books.Count == 0)
        {
            Console.WriteLine("No books available.");
            return;
        }

        foreach (var book in books)
        {
            Console.WriteLine(book.GetInfo());
        }
    }

    private void SearchBooks()
    {
        Console.Write("Enter search query: ");
        string? query = Console.ReadLine();
        var results = _catalog.FindBooks(query ?? "");
        if (results.Count == 0)
        {
            Console.WriteLine("No books found.");
            return;
        }

        foreach (var book in results)
        {
            Console.WriteLine(book.GetInfo());
        }
    }

    private void BorrowBook()
    {
        Console.Write("Enter book ID to borrow: ");
        if (int.TryParse(Console.ReadLine(), out int bookId))
        {
            var book = _catalog.GetBookById(bookId);
            if (book == null)
            {
                Console.WriteLine("Book not found.");
                return;
            }

            book.Borrow(_user);
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }
    }

    private void ReturnBook()
    {
        Console.Write("Enter book ID to return: ");
        if (int.TryParse(Console.ReadLine(), out int bookId))
        {
            var book = _catalog.GetBookById(bookId);
            if (book == null)
            {
                Console.WriteLine("Book not found.");
                return;
            }

            if (book.Return(_user))
                _notifier.Notify(book.Id, book.Title);
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }
    }

    private void AddToWishlist()
    {
        Console.Write("Enter book ID to add to wishlist: ");
        if (int.TryParse(Console.ReadLine(), out int bookId))
        {
            var book = _catalog.GetBookById(bookId);
            if (book == null)
            {
                Console.WriteLine("Book not found.");
                return;
            }

            if (book.State is AvailableState)
            {
                Console.WriteLine("This book is already available!");
                return;
            }
            if (_user.BorrowedBooks.Any(b => b.Id == bookId))
            {
                Console.WriteLine("You already borrowed this book!");
                return;
            }
            else if (_user is ISubscriber subscriber)
            {
                _notifier.Subscribe(book.Id, subscriber);
                Console.WriteLine("Book added to wishlist. You'll be notified when it becomes available.");
            }
        }
        else
        {
            Console.WriteLine("Invalid ID.");
        }
    }
}