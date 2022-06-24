namespace SqlBob;

public sealed class Fetch : SqlStatement
{
    /// <summary>Creates a new instance of the <see cref="Fetch"/>class.</summary>
    internal Fetch(SqlStatement count) => Count = count;

    /// <summary>The SQL expression to parenthesize.</summary>
    public SqlStatement Count { get; }

    /// <inheritdoc />
    public override void Write(SqlBuilder builder, int depth)
        => Guard.NotNull(builder, nameof(builder))
        .Indent(depth)
        .Write(Keyword.FETCH)
        .Space()
        .Write(Keyword.NEXT)
        .Space()
        .Write(Count)
        .Space()
        .Write(Keyword.ROWS)
        .Space()
        .Write(Keyword.ONLY)
        .NewLineOrSpace();
}
