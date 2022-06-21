namespace SqlBob;

/// <summary>Represent a SQL alias.</summary>
[DebuggerDisplay("{DebuggerDisplay}")]
public readonly struct Alias : ISqlStatement
{
    /// <summary>A null/not set alias.</summary>
    public static readonly Alias None;

    /// <summary>Creates a new instance of a SQL <see cref="Alias"/>.</summary>
    public Alias(string value) => _value = value;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly string _value;

    /// <summary>Adds an alias to a SQL literal.</summary>
    [Pure]
    public Literal Add(Literal selector)
    {
        return NotEmpty()
            ? _value + '.' + selector.ToString()
            : selector;
    }

    /// <summary>Adds an alias to a SQL literal.</summary>
    public static Literal operator +(Alias alias, Literal selector) => alias.Add(selector);
    /// <summary>Adds an alias to a SQL literal.</summary>
    public static Literal operator +(Alias alias, string selector) => alias.Add(selector);

    /// <summary>Implicitly casts a <see cref="string"/> to a SQL <see cref="Alias"/>.</summary>
    public static implicit operator Alias(string value) => new(value);

    /// <inherritdoc/>
    public void Write(SqlBuilder builder, int depth = 0)
    {
        Guard.NotNull(builder, nameof(builder));
        builder.Literal(_value);
    }

    /// <summary>Has the alias been specified?</summary>
    [Pure]
    public bool NotEmpty() => !string.IsNullOrEmpty(_value);

    /// <inherritdoc/>
    [Pure]
    public override string ToString() => this.UseSqlBuilder();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    internal string DebuggerDisplay => this.GetDebuggerDisplay();
}
