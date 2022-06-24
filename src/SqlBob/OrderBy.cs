namespace SqlBob;

public sealed class OrderBy : SqlStatement
{
    internal OrderBy(SqlStatements? expressions) 
        => Expressions = expressions.Required(nameof(expressions));

    public SqlStatements Expressions { get; }

    /// <inheritdoc />
    public override void Write(SqlBuilder builder, int depth)
        => Guard.NotNull(builder, nameof(builder))
        .Indent(depth)
        .Write(Keyword.ORDER_BY)
        .NewLineOrSpace()
        .Join(
            ",",
            (qb, expression) => qb.NewLineOrSpace().Write(expression, depth + 1),
            Expressions)
        .NewLineOrSpace();

    [Pure]
    internal static OrderBy? Convert(params object[] args)
    {
        if (args is null || !args.Any()) return null;
        else if (args.Length == 1) return (OrderBy?)Convert(args[0], x => new OrderBy(SqlStatements.New(x)));
        else
        {
            var expressions = new List<SqlStatement>();
            foreach (var arg in args)
            {
                if (arg is OrderBy orderBy) expressions.AddRange(orderBy.Expressions);
                else if (arg is SqlStatements statements) expressions.AddRange(statements);
                else if (Convert(arg, x => Order.By(x)) is { } order) expressions.Add(order);
            }
            return expressions.Any() ? new OrderBy(SqlStatements.None.AddRange(expressions)) : null;
        }

    }
}
