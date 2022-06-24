namespace SqlBob;

public sealed class Order : SqlStatement
{
    [Pure]
    public static Order By(object expression) => new(SQL.Convert(expression).Required("expression"), true);

    internal Order(SqlStatement expression, bool ascending)
    {
        Expression = expression;
        Ascending = ascending;
    }

    public SqlStatement Expression { get; }
    public bool Ascending { get; }

    [Pure]
    public Order Asc() => !Ascending ? new(Expression, true) : this;

    [Pure]
    public Order Desc() => Ascending ? new(Expression, false): this;

    public override void Write(SqlBuilder builder, int depth)
      => Guard.NotNull(builder, nameof(builder))
        .Write(Expression, depth)
        .Space()
        .Write(Ascending ? Keyword.ASC : Keyword.DESC);

}
