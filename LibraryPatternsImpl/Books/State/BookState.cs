namespace LibraryPatternsImpl.Books.State;

using LibraryPatternsImpl.Books;
using LibraryPatternsImpl.Users;

public abstract class BookState(IBook book)
{
    protected IBook Book { get; } = book;

    public abstract bool Borrow(IUser user);
    public abstract bool Return(IUser user);
    public abstract string GetStateName();

}