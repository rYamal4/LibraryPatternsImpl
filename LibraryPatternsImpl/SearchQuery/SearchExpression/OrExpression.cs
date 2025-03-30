using LibraryPatternsImpl.Books;

namespace LibraryPatternsImpl.SearchQuery.SearchExpression;

public class OrExpression(AbstractExpression left, AbstractExpression right) : AbstractExpression
{
    private readonly AbstractExpression _left = left;
    private readonly AbstractExpression _right = right;

    public override List<IBook> Interpret(Context context)
    {
        var leftResult = _left.Interpret(context);
        var rightResult = _right.Interpret(context);
        return leftResult.Union(rightResult).ToList();
    }
}