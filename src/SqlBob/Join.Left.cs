namespace SqlBob;

public sealed class LeftJoin : Join
{
    internal LeftJoin(SqlStatement? expression, SqlStatement? condition) : base(expression, condition) { }

    protected override Keyword JoinType => "LEFT JOIN";

    [Pure]
    public LeftJoin On(object condition) => new(Table, SQL.Convert(condition));
}
