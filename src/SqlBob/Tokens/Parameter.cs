namespace SqlBob;

/// <summary>Represent a SQL keyword parameter.</summary>
[DebuggerDisplay("{DebuggerDisplay}")]
public readonly struct Parameter : ISqlStatement, IEquatable<Parameter>
{
    /// <summary>Creates a new instance of a <see cref="Parameter"/>.</summary>
    public Parameter(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Value = null;
        }
        else
        {
            Value = value[0] == '@'
                ? value
                : '@' + value;
        }
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly string Value;

    /// <summary>Implicitly casts a <see cref="string"/> to a <see cref="Parameter"/>.</summary>
    public static implicit operator Parameter(string value) => new(value);

    /// <inherritdoc/>
    public void Write(SqlBuilder builder, int depth)
    {
        Guard.NotNull(builder, nameof(builder));

        builder.Literal(Value);
    }

    /// <summary>Represents the <see cref="Parameter"/> as <see cref="string"/>.</summary>
    [Pure]
    public override string ToString() => this.UseSqlBuilder();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    internal string DebuggerDisplay => this.GetDebuggerDisplay();

    /// <inherritdoc/>
    [Pure]
    public override bool Equals(object obj) => obj is Parameter other && Equals(other);

    /// <inherritdoc/>
    [Pure]
    public bool Equals(Parameter other) => string.Equals(Value, other.Value, StringComparison.InvariantCultureIgnoreCase);

    /// <inherritdoc/>
    [Pure]
    public override int GetHashCode() => Value is null ? 0 : Value.ToUpperInvariant().GetHashCode();
}
