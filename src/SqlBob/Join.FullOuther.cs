namespace SqlBob;

public sealed class FullOutherJoin : Join
{
    internal FullOutherJoin(SqlStatement? expression, SqlStatement? condition) : base(expression, condition) { }

    protected override Keyword JoinType => "FULL OUTHER JOIN";

    [Pure]
    public FullOutherJoin On(object condition) => new(Table, SQL.Convert(condition));
}
