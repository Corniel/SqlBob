namespace SqlBob;

public sealed class From : SqlStatement
{
    /// <summary>Creates a new instance of the <see cref="From"/>class.</summary>
    internal From(SqlStatement table) => Table = table;

    /// <summary>The table to select from.</summary>
    public SqlStatement Table { get; }

    /// <inheritdoc />
    public override void Write(SqlBuilder builder, int depth)
        => Guard.NotNull(builder, nameof(builder))
        .Indent(depth)
        .Write(Keyword.FROM)
        .Space()
        .Write(Table)
        .NewLineOrSpace();
}
