namespace LibraryPatternsImpl.Books;

public class BestsellerBook(IBook book) : AbstractBookDecorator(book)
{
    public override string GetInfo() => $"{_book.GetInfo()} [Bestseller]";
}
