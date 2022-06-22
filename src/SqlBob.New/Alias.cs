namespace SqlBob;

/// <summary>Represent a SQL alias.</summary>
[DebuggerDisplay("{DebuggerDisplay}")]
public readonly struct Alias
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly string? Value;

    /// <summary>A null/not set alias.</summary>
    public static readonly Alias None;

    /// <summary>Creates a new instance of a SQL <see cref="Alias"/>.</summary>
    private Alias(string? value) => Value = value == string.Empty ? null : value;

    /// <summary>Implicitly casts a <see cref="string"/> to a SQL <see cref="Alias"/>.</summary>
    public static implicit operator Alias(string? value) => new(value);

    /// <summary>True when the alias has been specified.</summary>
    [Pure]
    public bool NotEmpty() => !string.IsNullOrEmpty(Value);

    /// <inherritdoc/>
    [Pure]
    public override string ToString() => Value ?? string.Empty;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    internal string DebuggerDisplay => Value ?? "{}";
}
