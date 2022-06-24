namespace SqlBob.Formatting;

/// <summary>A <see cref="StringBuilder"/> based SQL (string) builder.</summary>
public sealed class SqlBuilder
{
    internal static readonly SqlBuilderPool Pool = new();

    private readonly StringBuilder sb = new();

    /// <summary>Creates a new instance of a <see cref="SqlBuilder"/>.</summary>
    public SqlBuilder() : this(null) { }

    /// <summary>Creates a new instance of a <see cref="SqlBuilder"/>.</summary>
    public SqlBuilder(SqlFormatInfo? formatInfo) => FormatInfo = formatInfo ?? SqlFormatInfo.Debugger;

    /// <summary>Gets and sets the format info.</summary>
    public SqlFormatInfo FormatInfo { get; internal set; }

    /// <summary>Writes a literal <see cref="string"/>.</summary>
    [FluentSyntax]
    public SqlBuilder Literal(string? value)
    {
        sb.Append(value);
        return this;
    }

    /// <summary>Writes a literal <see cref="ch"/>.</summary>
    [FluentSyntax]
    public SqlBuilder Literal(char value)
    {
        sb.Append(value);
        return this;
    }

    /// <summary>Writes a SQL statement.</summary>
    /// <param name="statement">
    /// The SQL statement to write.
    /// </param>
    /// <param name="depth"></param>
    [FluentSyntax]
    public SqlBuilder Write(SqlStatement statement) => Write(statement, 0);

    /// <summary>Writes a SQL statement.</summary>
    /// <param name="statement">
    /// The SQL statement to write.
    /// </param>
    /// <param name="depth"></param>
    [FluentSyntax] 
    public SqlBuilder Write(SqlStatement statement, int depth)
    {
        Guard.NotNull(statement, nameof(statement));
        statement.Write(this, depth);
        return this;
    }
    
    [FluentSyntax]
    public SqlBuilder Write(Alias alias)
    {
        sb.Append(alias);
        return this;
    }

    [FluentSyntax]
    public SqlBuilder Write(Keyword keyword)
    {
        sb.Append(keyword.ToString(FormatInfo));
        return this;
    }

    [FluentSyntax]
    public SqlBuilder Write(Schema schema)
    {
        sb.Append(schema);
        return this;
    }

    /// <summary>Concatenates all SQL statements using the specified
    /// separator between each statement. 
    /// </summary>
    /// <param name="separator">
    /// The separator to write.
    /// </param>
    /// <param name="write">
    /// The write action to apply on every statements.
    /// </param>
    /// <param name="statements">
    /// The statements to join.
    /// </param>
    [FluentSyntax]
    public SqlBuilder Join(object separator, Action<SqlBuilder, SqlStatement> write, IEnumerable<SqlStatement> statements)
    {
        Guard.NotNull(statements, nameof(statements));

        var index = 0;
        foreach(var statement in statements)
        {
            if (index++ != 0)
            {
                Write(SQL.Convert(separator)!);
            }
            write(this, statement);
        }
        return this;
    }

    /// <summary>Writes the indent.</summary>
    [FluentSyntax]
    public SqlBuilder Indent(int depth)
    {
        if (FormatInfo.UseNewLine)
        {
            for (var i = 0; i < depth; i++)
            {
                sb.Append(FormatInfo.Indent);
            }
        }
        return this;
    }

    /// <summary>Writes a single space.</summary>
    [FluentSyntax]
    public SqlBuilder Dot() => Literal(".");

    /// <summary>Writes a single space.</summary>
    [FluentSyntax]
    public SqlBuilder Space()
    {
        if (sb.Length != 0 && sb[^1] != ' ')
        {
            Literal(" ");
        }
        return this;
    }

    /// <summary>Writes a new line or a space depending on the settings.</summary>
    [FluentSyntax]
    public SqlBuilder NewLineOrSpace()
    {
        if (FormatInfo.UseNewLine)
        {
            sb.AppendLine();
        }
        else
        {
            Space();
        }
        return this;
    }

    /// <summary>Writes a new line or a nothing depending on the settings.</summary>
    [FluentSyntax]
    public SqlBuilder NewLine()
    {
        if (FormatInfo.UseNewLine)
        {
            sb.AppendLine();
        }
        return this;
    }

    /// <summary>Returns the constructed SQL as <see cref="string"/>.</summary>
    [Pure]
    public override string ToString()
    {
        // Trims the end.
        while (sb.Length != 0 && sb[^1] == ' ')
        {
            sb.Remove(sb.Length - 1, 1);
        }
        SyntaxErrorHandling();

        return sb.ToString();
    }

    /// <summary>Throw when it should throw and has a syntax error.</summary>
    private void SyntaxErrorHandling()
    {
        if (Throw && HasSyntaxError)
        {
            var error = new SyntaxError($"SQL contains a syntax error: {sb}");
            Pool.Push(this);
            throw error;
        }
    }

    /// <summary>Shortcut for <see cref="SqlFormatInfo.Throw"/>.</summary>
    internal bool Throw => FormatInfo.Throw;
    internal bool IsDebug => FormatInfo == SqlFormatInfo.Debugger;
    internal int Capacity => sb.Capacity;

    /// <summary>True if an syntax error was rendered.</summary>
    internal bool HasSyntaxError { get; set; }

    /// <summary>Clears the state of the builder.</summary>
    internal void Clear()
    {
        sb.Clear();
        HasSyntaxError = false;
    }
}
