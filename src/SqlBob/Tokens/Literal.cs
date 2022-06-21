namespace SqlBob;

/// <summary>Represents <see cref="string"/> that is a SQL statement.</summary>
[DebuggerDisplay("{DebuggerDisplay}")]
public readonly struct Literal : ISqlStatement
{
    /// <summary>A null/not set alias.</summary>
    public static readonly Literal None;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly string Value;

    /// <summary>Creates a new instance of a SQL <see cref="Literal"/>.</summary>
    private Literal(string value) => Value = value == string.Empty ? null : value;

    /// <summary>Implicitly casts a <see cref="string"/> to a <see cref="Literal"/>.</summary>
    public static implicit operator Literal(string value) => new(value);

    /// <inherritdoc/>
    public void Write(SqlBuilder builder, int depth)
    {
        Guard.NotNull(builder, nameof(builder));
        builder.Indent(depth).Literal(Value);
    }

    /// <inherritdoc/>
    [Pure]
    public override string ToString() => this.UseSqlBuilder();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    internal string DebuggerDisplay => this.GetDebuggerDisplay();
}
