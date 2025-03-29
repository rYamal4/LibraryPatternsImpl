using System.Text.RegularExpressions;

namespace LibraryPatternsImpl.Catalog;

public class SearchInterpreter
{
    public Dictionary<string, object> Interpret(string query)
    {
        var filters = new Dictionary<string, object>();
        var parts = query.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var part in parts)
        {
            if (part.Contains(":"))
            {
                var pair = part.Split(':');
                filters[pair[0]] = pair[1];
            }
            else if (Regex.IsMatch(part, @"\w+[><=]+\d+"))
            {
                var match = Regex.Match(part, @"(\w+)([><=]+)(\d+)");
                filters[match.Groups[1].Value] = new Func<int, bool>(x =>
                    EvaluateCondition(x, match.Groups[2].Value, int.Parse(match.Groups[3].Value)));
            }
        }
        return filters;
    }

    private bool EvaluateCondition(int value, string op, int target)
    {
        return op switch
        {
            ">=" => value >= target,
            "<=" => value <= target,
            ">" => value > target,
            "<" => value < target,
            "=" => value == target,
            _ => false
        };
    }
}
