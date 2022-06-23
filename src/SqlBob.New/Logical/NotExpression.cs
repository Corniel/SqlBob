namespace SqlBob;

/// <summary>Represent a SQL NOT statement.</summary>
public sealed class NotExpression : SqlStatement
{
    internal NotExpression(SqlStatement expression)=> Expression = expression;

    public SqlStatement Expression { get; }

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
