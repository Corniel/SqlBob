namespace SqlBob;

public sealed class RightJoin : Join
{
    internal RightJoin(SqlStatement? expression, SqlStatement? condition) : base(expression, condition) { }

    protected override Keyword JoinType => "RIGHT JOIN";

    public RightJoin On(object condition) => new(Table, SQL.Convert(condition));
}
