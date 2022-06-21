namespace SqlBob;

/// <summary>Represents <see cref="string"/> that is a SQL statement.</summary>
[DebuggerDisplay("{DebuggerDisplay}")]
public readonly struct Literal : ISqlStatement
{
    /// <summary>Creates a new instance of a SQL <see cref="Literal"/>.</summary>
    public Literal(string value) => _value = value;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly string _value;

    /// <summary>Implicitly casts a <see cref="string"/> to a <see cref="Literal"/>.</summary>
    public static implicit operator Literal(string value) => new Literal(value);

    /// <inherritdoc/>
    public void Write(SqlBuilder builder, int depth = 0)
    {
        Guard.NotNull(builder, nameof(builder));
        builder.Indent(depth).Literal(_value);
    }

    /// <inherritdoc/>
    [Pure]
    public override string ToString() => this.UseSqlBuilder();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    internal string DebuggerDisplay => this.GetDebuggerDisplay();
}
