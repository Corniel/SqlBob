namespace SqlBob;

public static class SQL
{
    [Pure]
    public static Alias Alias(string alias) => alias;

    [Pure]
    public static Keyword Keyword(string keyword) => keyword;

    [Pure]
    public static Literal Literal(string literal) => literal;

    [Pure]
    public static Parameter Parameter(string parameter) => parameter;


    /// <summary>Converts an object to a SQL statement.</summary>
    [Pure]
    internal static T Convert<T>(object arg, Func<object, T> converter) where T : ISqlStatement
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
    internal static ISqlStatement Convert(object arg)
        => Convert<ISqlStatement>(arg, (input) => (Literal)input.ToString());
}
