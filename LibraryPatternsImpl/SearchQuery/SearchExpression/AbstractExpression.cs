using LibraryPatternsImpl.Books;

namespace LibraryPatternsImpl.SearchQuery.SearchExpression;

public abstract class AbstractExpression
{
    public abstract List<IBook> Interpret(Context context);
}
