namespace SqlBob;

public sealed class None : SqlStatement
{
    internal None() { }
    public override void Write(SqlBuilder builder, int depth) => Guard.NotNull(builder, nameof(builder));
}
