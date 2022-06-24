namespace SqlBob;

public sealed class Select : SqlStatement
{
    internal Select(SqlStatements? selections) 
        => Selections = selections.Required(nameof(selections));

    public SqlStatements Selections { get; }

    /// <inheritdoc />
    public override void Write(SqlBuilder builder, int depth)
        => Guard.NotNull(builder, nameof(builder))
       .Indent(depth)
        .Write(Keyword.SELECT)
        .Join(
            ",",
            (qb, select) => qb.NewLineOrSpace().Indent(depth + 1).Write(select, 0),
            Selections)
        .NewLineOrSpace();

    [Pure]
    internal static Select? Convert(params object[] args)
    {
        if (args is null || !args.Any()) return null;
        else
        {
            var expressions = new List<SqlStatement>();
            foreach (var arg in args)
            {
                if (arg is Select select) expressions.AddRange(select.Selections);
                else if (arg is SqlStatements statements) expressions.AddRange(statements);
                else if (Convert(arg, x => new Selection(x, default)) is { } selection) expressions.Add(selection);
            }
            return expressions.Any() ? new Select(SqlStatements.None.AddRange(expressions)) : null;
        }

    }
}
