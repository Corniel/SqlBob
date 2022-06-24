namespace SqlBob;

internal static class Guarding
{
    private static readonly None Nill = new();

    [Pure]
    public static SqlStatement NotNull(this SqlStatement? statement)
        => statement ?? Nill;

    [Pure]
    public static SqlStatement Required(this SqlStatement? statement, string message)
        => statement is not null && statement is not None
        ? statement
        : new MissingSql(message);

    private sealed class None : SqlStatement
    {
        [Pure]
        public override SqlStatement Not() => this.Required("expression");

        [Pure]
        public override Selection As(Alias alias) => new(null, alias);

        public override void Write(SqlBuilder builder, int depth) => Guard.NotNull(builder, nameof(builder));
    }
}
