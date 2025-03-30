using LibraryPatternsImpl.Books;
using LibraryPatternsImpl.Catalog;

namespace LibraryPatternsImpl.Utils;

public static class BookStorage
{
    public static void Populate(ICatalog catalog)
    {
        var books = new List<IBook>
        {
            new Book(1, "1984", "George Orwell", 1949, "Dystopian"),
            new Book(2, "Brave New World", "Aldous Huxley", 1932, "Science Fiction"),
            new Book(3, "The Hobbit", "J.R.R. Tolkien", 1937, "Fantasy"),
            new Book(4, "Fahrenheit 451", "Ray Bradbury", 1953, "Dystopian"),
            new Book(5, "To Kill a Mockingbird", "Harper Lee", 1960, "Classic"),
            new Book(6, "The Great Gatsby", "F. Scott Fitzgerald", 1925, "Classic"),
            new Book(7, "Moby Dick", "Herman Melville", 1851, "Adventure"),
            new Book(8, "War and Peace", "Leo Tolstoy", 1869, "Historical"),
            new Book(9, "Crime and Punishment", "Fyodor Dostoevsky", 1866, "Philosophical"),
            new Book(10, "Harry Potter and the Philosopher's Stone", "J.K. Rowling", 1997, "Fantasy"),
            new Book(11, "The Catcher in the Rye", "J.D. Salinger", 1951, "Classic"),
            new Book(12, "Pride and Prejudice", "Jane Austen", 1813, "Romance"),
            new Book(13, "The Lord of the Rings", "J.R.R. Tolkien", 1954, "Fantasy"),
            new Book(14, "The Alchemist", "Paulo Coelho", 1988, "Adventure"),
            new Book(15, "The Da Vinci Code", "Dan Brown", 2003, "Thriller")
        };
        books[0] = new BestsellerBook(books[0]);
        books[7] = new ReservedBook(books[7]);
        books[11] = new ReservedBook(books[11]);

        foreach (var book in books)
        {
            catalog.AddBook(book);
        }
    }
}