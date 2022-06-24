namespace SqlBob;

public sealed class Where : SqlStatement
{
    internal Where(SqlStatement? condition) => this.Condition = condition.Required(nameof(condition));

    public SqlStatement Condition { get; }

    /// <inheritdoc />
    public override void Write(SqlBuilder builder, int depth)
        => Guard.NotNull(builder, nameof(builder))
        .Indent(depth)
        .Write(Keyword.WHERE)
        .NewLineOrSpace()
        .Write(Condition, depth + 1)
        .NewLineOrSpace();
}
