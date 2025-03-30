using LibraryPatternsImpl.Books;

namespace LibraryPatternsImpl.SearchQuery.SearchExpression;

public class TerminalExpression(Func<IBook, bool> predicate) : AbstractExpression
{
    private readonly Func<IBook, bool> _predicate = predicate;

    public override List<IBook> Interpret(Context context)
    {
        return context.Books.Where(_predicate).ToList();
    }
}
