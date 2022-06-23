namespace SqlBob;

[DebuggerDisplay("{DebuggerDisplay}")]
public abstract class SqlStatement
{
    /// <summary>Writes the SQL statement to the SQL builder buffer.</summary>
    /// <param name="builder">
    /// The SQL builder.
    /// </param>
    /// <param name="depth">
    /// The nested depth.
    /// </param>
    public abstract void Write(SqlBuilder builder, int depth);

    /// <summary>Writes the SQL statement to the SQL builder buffer.</summary>
    /// <param name="builder">
    /// The SQL builder.
    /// </param>
    public void Write(SqlBuilder builder) => Write(builder, 0);

    [Pure]
    public virtual Selection As(Alias alias) => new(this, alias);

    [Pure]
    public virtual SqlStatement Not() => new Negate(this);

    public Compare Eq(object expression) => new(this, "=", SQL.Convert(expression) ?? SQL.Missing("expression"));
    public Compare Ne(object expression) => new(this, "<>", SQL.Convert(expression) ?? SQL.Missing("expression"));
    public Compare Lt(object expression) => new(this, "<", SQL.Convert(expression) ?? SQL.Missing("expression"));
    public Compare Lte(object expression) => new(this, "<=", SQL.Convert(expression) ?? SQL.Missing("expression"));
    public Compare Gt(object expression) => new(this, ">", SQL.Convert(expression) ?? SQL.Missing("expression"));
    public Compare Gte(object expression) => new(this, ">=", SQL.Convert(expression) ?? SQL.Missing("expression"));

    [Pure]
    public Parenthesis Parenthesis() => this as Parenthesis ?? new(this);

    /// <summary>Gets a minified representation of th SQL statement.</summary>
    /// <param name="sqlStatement">
    /// The SQL statement to represent as <see cref="string"/>.
    /// </param>
    [Pure]
    public string Minified() => ToString(SqlFormatInfo.Minified);

    /// <summary>Gets a <see cref="string"/> representation of the SQL statement.</summary>
    /// <param name="formatInfo">
    /// The format info to use.
    /// </param>
    /// <remarks>
    /// Uses the <see cref="SqlBuilderPool"/> to minimize <see cref="SqlBuilder"/>
    /// (and by that <see cref="StringBuilder"/>) construction and
    /// destruction.
    /// </remarks>
    [Pure]
    public string ToString(SqlFormatInfo? formatInfo)
    {
        var builder = SqlBuilder.Pool.Pop();
        builder.FormatInfo = formatInfo ?? SqlFormatInfo.Debugger;
        Write(builder);
        var sql = builder.ToString();
        SqlBuilder.Pool.Push(builder);
        return sql;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    internal string DebuggerDisplay
        => $"{{{GetType().Name}}} {ToString(SqlFormatInfo.Debugger)}";
}
