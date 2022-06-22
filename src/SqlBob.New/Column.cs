namespace SqlBob;

public sealed record Column : SqlStatement
{
    internal Column(Table table, string name)
    {
        Table = table;
        Name = name;
    }

    public Table Table { get; }
    public string Name { get; }

    /// <inheritdoc />
    public override void Write(SqlBuilder builder, int depth)
    {
        Guard.NotNull(builder, nameof(builder)).Indent(depth);

        if (Table.Alias.NotEmpty())
        {
            builder.Write(Table.Alias).Dot().Literal(Name);
        }
        else
        {
            builder.Literal(Table.Name).Dot().Literal(Name);
        }
    }
}
