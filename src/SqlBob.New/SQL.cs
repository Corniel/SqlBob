namespace SqlBob;

public static class SQL
{
    public static readonly None None = new();

    [Pure]
    public static SyntaxError Error(string message) => new(Guard.NotNullOrEmpty(message, nameof(message)));

    [Pure]
    public static Raw Raw(string? sql) => new(sql);

    [Pure]
    public static Selection Select(object expression) => new(Convert(expression ?? Error("")), Alias.None);

    /// <summary>Converts a collection of objects to a collection of SQL statements.</summary>
    [Pure]
    internal static T[] ConvertAll<T>(object[] args, Func<object, T> converter) where T : SqlStatement
    {
        if (args is null) return Array.Empty<T>();
        else
        {
            var statements = new T[args.Length];

            var size = 0;

            for (var i = 0; i < statements.Length; i++)
            {
                var statement = Convert(args[i], converter);

                if (!Equals(statement, default(T)))
                {
                    statements[size++] = statement;
                }
            }
            return size == statements.Length
                ? statements
                : statements.Take(size).ToArray();
        }
    }

    /// <summary>Converts a collection of objects to a collection of SQL statements.</summary>
    [Pure]
    internal static SqlStatement[] ConvertAll(params object[] args)
        => ConvertAll<SqlStatement>(args, (input) => (Raw)input.ToString());

    /// <summary>Converts an object to a SQL statement.</summary>
    [Pure]
    internal static T? Convert<T>(object arg, Func<object, T> converter) where T : SqlStatement
    {
        if (arg is null || string.Empty.Equals(arg))
        {
            return default;
        }
        else return (arg is T statement)
            ? statement
            : converter(arg);
    }

    /// <summary>Converts an object to a SQL statement.</summary>
    [Pure]
    internal static SqlStatement Convert(object arg) => arg as SqlStatement ?? Raw(arg.ToString());
}
