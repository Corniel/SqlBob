namespace SqlBob;

/// <summary>Represent a SQL NOT statement.</summary>
public sealed class Negate : SqlStatement
{
    internal Negate(SqlStatement expression)=> Expression = expression;

    public SqlStatement Expression { get; }

    [Pure]
    public override SqlStatement Not() => Expression;

    /// <inheritdoc/>
    public override void Write(SqlBuilder builder, int depth)
        => Guard.NotNull(builder, nameof(builder))
        .Indent(depth)
        .Write(Keyword.NOT)
        .Literal("(")
        .NewLine()
        .Write(Expression, depth + 1)
        .NewLine()
        .Indent(depth)
        .Literal(")")
        .NewLine()
    ;
}
