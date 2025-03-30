using LibraryPatternsImpl.Books.State;
using LibraryPatternsImpl.Users;

namespace LibraryPatternsImpl.Books;

public class Book : IBook
{
    public int Id { get; }
    public string Title { get; }
    public string Author { get; }
    public int Year { get; }
    public string Genre { get; }

    public BookState State { get; set; }
    public IUser? Owner { get; set; }

    public Book(int id, string title, string author, int year, string genre)
    {
        Id = id;
        Title = title;
        Author = author;
        Year = year;
        Genre = genre;
        State = new AvailableState(this);
    }

    public string GetInfo() =>
        $"[{Id}] {Title} by {Author} ({Year}) - {Genre} | State: {State.GetStateName()}";

    public bool Borrow(IUser user) => State.Borrow(user);

    public bool Return(IUser user) => State.Return(user);
}
