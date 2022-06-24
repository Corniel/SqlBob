namespace SqlBob;

/// <summary>Represent a SQL keyword token.</summary>
[DebuggerDisplay("{DebuggerDisplay}")]
public readonly struct Keyword : IEquatable<Keyword>
{
    /// <summary>ASC (ORDER BY expression ASC).</summary>
    public static readonly Keyword ASC = "ASC";

    /// <summary>AND (expression_0 AND expression_1).</summary>
    public static readonly Keyword AND = "AND";

    /// <summary>AS (expression AS alias).</summary>
    public static readonly Keyword AS = "AS";

    /// <summary>ASC (ORDER BY expression DESC).</summary>
    public static readonly Keyword DESC = "DESC";

    /// <summary>ONLY (OFFSET @skip ROWS FETCH NEXT @rows ROWS ONLY).</summary>
    public static readonly Keyword FETCH = "FETCH";

    /// <summary>GROUP BY (GROUP BY expression).</summary>
    public static readonly Keyword GROUP_BY = "GROUP BY";

    /// <summary>FROM (SELECT expression FROM table).</summary>
    public static readonly Keyword FROM = "FROM";

    /// <summary>NEXT (OFFSET @skip ROWS FETCH NEXT @rows ROWS ONLY).</summary>
    public static readonly Keyword NEXT = "NEXT";

    /// <summary>NOT (NOT expression).</summary>
    public static readonly Keyword NOT = "NOT";

    /// <summary>OFFSET (OFFSET @skip ROWS FETCH NEXT @rows ROWS ONLY).</summary>
    public static readonly Keyword OFFSET = "OFFSET";

    /// <summary>ON (JOIN table1 ON table0.column = table1.column).</summary>
    public static readonly Keyword ON = "ON";

    /// <summary>ONLY (OFFSET @skip ROWS FETCH NEXT @rows ROWS ONLY).</summary>
    public static readonly Keyword ONLY = "ONLY";

    /// <summary>OR (expression_0 OR expression_1).</summary>
    public static readonly Keyword OR = "OR";

    /// <summary>ORDER BY (ORDER BY expression).</summary>
    public static readonly Keyword ORDER_BY = "ORDER BY";

    /// <summary>ONLY (OFFSET @skip ROWS FETCH NEXT @rows ROWS ONLY).</summary>
    public static readonly Keyword ROWS = "ROWS";

    /// <summary>SELECT (SELECT expression).</summary>
    public static readonly Keyword SELECT = "SELECT";

    /// <summary>WHERE (WHERE expression).</summary>
    public static readonly Keyword WHERE = "WHERE";

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly string? Value;

    /// <summary>Creates a new instance of a <see cref="Keyword"/>.</summary>
    private Keyword(string? value) => Value = value == string.Empty ? null : value;

    /// <summary>Implicitly casts a <see cref="string"/> to a <see cref="Keyword"/>.</summary>
    public static implicit operator Keyword(string? value) => new(value);

    /// <summary>Represents the <see cref="Keyword"/> as <see cref="string"/>.</summary>
    [Pure]
    public override string ToString() => ToString(null);

    /// <summary>Represents the <see cref="Keyword"/> as <see cref="string"/>.</summary>
    [Pure]
    public string ToString(SqlFormatInfo? info)
    {
        info ??= SqlFormatInfo.Debugger;
        return info.UseUpperCase
            ? (Value ?? string.Empty).ToUpperInvariant()
            : (Value ?? string.Empty).ToLowerInvariant();
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    internal string DebuggerDisplay => Value ?? "{}";

    /// <inherritdoc/>
    [Pure]
    public override bool Equals(object? obj) => obj is Keyword other && Equals(other);

    /// <inherritdoc/>
    [Pure]
    public bool Equals(Keyword other) => string.Equals(Value, other.Value);

    /// <inherritdoc/>
    [Pure]
    public override int GetHashCode() => Value is null ? 0 : Value.GetHashCode();
}
