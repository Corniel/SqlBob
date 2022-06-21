namespace SqlBob;

/// <summary>SQL Query builder.</summary>
public static class Query
{
    /// <summary>Creates a SELECT query.</summary>
    [Pure]
    public static SelectQuery Select(params object[] expressions) 
        => new()
        {
            SelectClause =
                (
                    expressions != null &&
                    expressions.Length == 1 &&
                    expressions[0] is SelectClause clause
                )
                    ? clause
                    : new SelectClause(expressions)
        };
}
