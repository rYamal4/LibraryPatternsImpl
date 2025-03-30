using LibraryPatternsImpl.Books.State;
using LibraryPatternsImpl.Users;

namespace LibraryPatternsImpl.Books;

public abstract class AbstractBookDecorator(IBook book) : IBook
{
    protected IBook _book = book;

    public virtual int Id => _book.Id;
    public virtual string Title => _book.Title;
    public virtual string Author => _book.Author;
    public virtual int Year => _book.Year;
    public virtual string Genre => _book.Genre;
    public virtual BookState State
    {
        get => _book.State;
        set => _book.State = value;
    }

    public virtual string GetInfo() => _book.GetInfo();

    public virtual bool Borrow(IUser user) => _book.Borrow(user);

    public virtual bool Return(IUser user) => _book.Return(user);
}