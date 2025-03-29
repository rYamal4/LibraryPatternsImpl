namespace LibraryPatternsImpl.Books;

public class Book(int id, string title, string author, int year, string genre) : IBook
{
    public int Id { get; } = id;
    public string Title { get; } = title;
    public string Author { get; } = author;
    public int Year { get; } = year;
    public string Genre { get; } = genre;

    public string GetInfo() => $"{Title} ({Author}), {Year}";
}
