namespace SqlBob;

/// <summary>Represents a SQL function.</summary>
public sealed class SqlFunction : SqlStatement
{
    /// <summary>Returns a Count(*) SQL function.</summary>
    [Pure]
    public static SqlFunction Count()
        => new(nameof(Count), SqlStatements.None.Add(SQL.Raw("*")));

    /// <summary>Returns a Count() SQL function.</summary>
    [Pure]
    public static SqlFunction Count(object expression) 
        => new(nameof(Count), SqlStatements.None.Add(SQL.Convert(expression)));

    /// <summary>Returns a GetDate() SQL function.</summary>
    [Pure]
    public static SqlFunction GetDate() => new(nameof(GetDate), SqlStatements.None);

    /// <summary>Returns a GetUtcDate() SQL function.</summary>
    [Pure]
    public static SqlFunction GetUtcDate() => new(nameof(GetUtcDate), SqlStatements.None);

    /// <summary>Creates a new instance of a <see cref="SqlFunction"/>.</summary>
    internal SqlFunction(string name, SqlStatements arguments)
    {
        Name = name;
        Arguments = arguments;
    }

    /// <summary>Gets the name of the SQL function.</summary>
    public string Name { get; }

    /// <summary>Gets the arguments of the SQL function.</summary>
    public SqlStatements Arguments { get; }

    /// <inherritdoc/>
    public override void Write(SqlBuilder builder, int depth)
        =>  Guard.NotNull(builder, nameof(builder))
        .Indent(depth)
        .Literal(Name)
        .Literal("(")
        .Join
        (
            ", ",
            (qb, statement) => qb.Write(statement, 0),
            Arguments
        )
        .Literal(")")
        .NewLineOrSpace()
    ;
}
