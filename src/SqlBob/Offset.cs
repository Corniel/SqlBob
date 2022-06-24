namespace SqlBob;

public sealed class Offset : SqlStatement
{
    /// <summary>Creates a new instance of the <see cref="Offset"/>class.</summary>
    internal Offset(SqlStatement count) => Count = count;

    /// <summary>The SQL expression to parenthesize.</summary>
    public SqlStatement Count { get; }

    /// <inheritdoc />
    public override void Write(SqlBuilder builder, int depth)
        => Guard.NotNull(builder, nameof(builder))
        .Indent(depth)
        .Write(Keyword.OFFSET)
        .Space()
        .Write(Count)
        .Space()
        .Write(Keyword.ROWS)
        .NewLineOrSpace();
}
