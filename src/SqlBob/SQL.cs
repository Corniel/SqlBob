﻿namespace SqlBob;

public static class SQL
{
    [Pure]
    public static Parameter Parameter(string? sql) => new(sql);

    [Pure]
    public static SqlFunction Function(string name, params object[] args)
        => new(name,SqlStatements.None.AddRange( ConvertAll(args)));

    [Pure]
    public static Raw Raw(string? sql) => new(sql);

    [Pure]
    public static Selection Select(object expression) 
        => new(Convert(expression).Required("select statement"), Alias.None);
    
    /// <summary>Converts a collection of objects to a collection of SQL statements.</summary>
    [Pure]
    internal static IEnumerable<SqlStatement> ConvertAll(params object[] args)
        => args.Where(arg => arg is { })
        .Select(arg => arg as SqlStatement ?? Raw(arg.ToString()));

    /// <summary>Converts an object to a SQL statement.</summary>
    [Pure]
    internal static SqlStatement? Convert(object? arg) 
        => arg is null
        ? null
        : arg as SqlStatement ?? Raw(arg.ToString());
}
