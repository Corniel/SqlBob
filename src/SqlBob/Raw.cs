namespace SqlBob;

public sealed class Raw : SqlStatement
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly string? Value;

    internal Raw(string? value) => Value = value;

    /// <inherritdoc/>
    public override void Write(SqlBuilder builder, int depth)
        => Guard.NotNull(builder, nameof(builder))
        .Indent(depth).Literal(Value);

    /// <summary>Implicitly casts a <see cref="string"/> to a <see cref="Raw"/>.</summary>
    public static implicit operator Raw(string value) => new(value);
}
