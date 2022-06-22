namespace SqlBob.Formatting;

/// <summary>Represents SQL formatting information.</summary>
public sealed class SqlFormatInfo
{
    internal static readonly SqlFormatInfo Minified = new() { UseNewLine = false, Indent = "", Throw = true };
    internal static readonly SqlFormatInfo Debugger = new() { UseNewLine = false, Indent = "", Throw = false };

    /// <summary>Use new line (default), or spaces to split statements.</summary>
    public bool UseNewLine { get; init; } = true;

    /// <summary>Use upper case for keywords (default).</summary>
    public bool UseUpperCase { get; init; } = true;

    /// <summary>The indent to apply (when UseNewLine = true).</summary>
    public string Indent { get; init; } = new string(' ', 4);

    /// <summary>Only to support <see cref="Debugger"/>.</summary>
    internal bool Throw { get; init; }
}
