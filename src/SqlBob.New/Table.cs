namespace SqlBob;

public sealed class Table : SqlStatement
{
    internal Table(Schema schema, string name, Alias alias)
    {
        Schema = schema;
        Name = name;
        Alias = alias;
    }

    public Schema Schema { get; }

    public SqlStatement[] Select(params object[] columns)
        => columns.Select(col => col switch
        {
            string name => Columns(name),
            SqlStatement sql => sql,
            _ => SQL.Raw(col?.ToString())
        })
        .ToArray();


    public string Name { get; }
    public Alias Alias { get; }

    public new Table As(Alias alias) => new(Schema, Name, alias);

    public Column Columns(string name) => new(this, name);

    /// <inheritdoc />
    public override void Write(SqlBuilder builder, int depth)
    {
        Guard.NotNull(builder, nameof(builder)).Indent(depth);

        if (Schema.NotEmpty())
        {
            builder.Write(Schema).Literal(".");
        }

        builder.Literal(Name);

        if (Alias.NotEmpty())
        {
            builder.Space().Write(Alias);
        }
    }
}
