namespace LibraryPatternsImpl.Books;

public class BestsellerDecorator(IBook book) : IBook
{
    private readonly IBook book = book;

    public int Id => book.Id;

    public string Title => book.Title;

    public string Author => book.Author;

    public int Year => book.Year;

    public string Genre => book.Genre;

    public string GetInfo() => $"{book.GetInfo()} [Бестселлер]";
}