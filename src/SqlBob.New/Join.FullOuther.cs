namespace SqlBob;

public sealed class FullOutherJoin : Join
{
    internal FullOutherJoin(SqlStatement? expression, SqlStatement? condition) : base(expression, condition) { }

    protected override Keyword JoinType => "FULL OUTHER JOIN";

    public FullOutherJoin On(object condition) => new(Expression, SQL.Convert(condition));
}
