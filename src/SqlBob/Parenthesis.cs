namespace SqlBob;

public sealed class Parenthesis : SqlStatement
{
    /// <summary>Creates a new instance of the <see cref="Parenthesis"/>class.</summary>
    internal Parenthesis(SqlStatement expression) => Expression = expression;

    /// <summary>The SQL expression to parenthesize.</summary>
    public SqlStatement Expression { get; }

    /// <inheritdoc />
    public override void Write(SqlBuilder builder, int depth)
        => Guard.NotNull(builder, nameof(builder))
        .Indent(depth)
        .Literal("(")
        .NewLine()
        .Write(Expression, depth + 1)
        .NewLine()
        .Indent(depth)
        .Literal(")")
        .NewLineOrSpace();
}
