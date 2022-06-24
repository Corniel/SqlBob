namespace SqlBob;

/// <summary>Represent a SQL alias.</summary>
[DebuggerDisplay("{DebuggerDisplay}")]
public readonly struct Schema
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly string? Value;

    /// <summary>No schema.</summary>
    public static readonly Schema None;

    /// <summary>No schema.</summary>
    public static readonly Schema Dbo = new("[dbo]");

    /// <summary>No schema.</summary>
    public static readonly Schema Sys = new("[sys]");

    /// <summary>No schema.</summary>
    public static readonly Schema Guest = new("[guest]");

    /// <summary>Creates a new instance of a SQL <see cref="Alias"/>.</summary>
    private Schema(string? value) => Value = value == string.Empty ? null : value;

    [Pure]
    public Table Table(string name) => new(this, name, Alias.None);

    /// <summary>Implicitly casts a <see cref="string"/> to a SQL <see cref="Schema"/>.</summary>
    public static implicit operator Schema(string? value) => new(value);

    /// <summary>True when the alias has been specified.</summary>
    [Pure]
    public bool NotEmpty() => !string.IsNullOrEmpty(Value);

    /// <inherritdoc/>
    [Pure]
    public override string ToString() => Value ?? string.Empty;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    internal string DebuggerDisplay => Value ?? "{}";
}
