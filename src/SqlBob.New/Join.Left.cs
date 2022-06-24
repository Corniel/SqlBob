namespace SqlBob;

public sealed class LeftJoin : Join
{
    internal LeftJoin(SqlStatement? expression, SqlStatement? condition) : base(expression, condition) { }

    protected override Keyword JoinType => "LEFT JOIN";

    public LeftJoin On(object condition) => new(Expression, SQL.Convert(condition));
}
