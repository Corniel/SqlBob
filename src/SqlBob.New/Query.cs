namespace SqlBob;

public sealed record Query : SqlStatement
{
    public static Query Select(params object[] selections) => new(selections, null, null);

    internal Query(object[] select, From from, Where where)
    {
        FromClause = from;
        WhereClause = where;
    }

    public From FromClause { get; }
    public Where WhereClause { get; }

    [Pure]
    public Query From(object expression)
        => new(Array.Empty<object>(), expression as From ?? SqlBob.From.Star, WhereClause);

    [Pure]
    public Query Where(object expression)
        => new(Array.Empty<object>(), FromClause, expression as Where ?? SqlBob.Where.None);

    public override void Write(SqlBuilder builder, int depth)
    {
        throw new NotImplementedException();
    }
}
