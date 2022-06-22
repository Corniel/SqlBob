namespace SqlBob;

public sealed record Order : SqlStatement
{
    public static Order By(object expression) => new(SQL.Convert(expression) ?? SQL.Missing("expression"), true);

    internal Order(SqlStatement expression, bool ascending)
    {
        Expression = expression;
        Ascending = ascending;
    }

    public SqlStatement Expression { get; }
    public bool Ascending { get; }

    public Order Asc() => !Ascending ? new(Expression, true) : this;
    
    public Order Desc() => Ascending ? new(Expression, false): this;

    public override void Write(SqlBuilder builder, int depth)
      => Guard.NotNull(builder, nameof(builder))
        .Write(Expression, depth)
        .Space()
        .Write(Ascending ? Keyword.ASC : Keyword.DESC);

}
