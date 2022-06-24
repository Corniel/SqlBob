namespace SqlBob;

public sealed class InnerJoin : Join
{
    internal InnerJoin(SqlStatement? expression, SqlStatement? condition) : base(expression, condition) { }

    protected override Keyword JoinType => "INNER JOIN";

    [Pure]
    public InnerJoin On(object condition) => new(Table, SQL.Convert(condition));
}
