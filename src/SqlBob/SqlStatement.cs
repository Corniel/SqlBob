namespace SqlBob;

/// <summary>The default implementation of <see cref="ISqlStatement"/>.</summary>
[DebuggerDisplay("{DebuggerDisplay}")]
public abstract class SqlStatement : ISqlStatement
{
    /// <inheritdoc/>
    public abstract void Write(SqlBuilder builder, int depth = 0);

    /// <inheritdoc/>
    [Pure]
    public sealed override string ToString() => this.UseSqlBuilder();

    /// <summary>Explicitly cast a <see cref="SqlStatement"/> to a <see cref="string"/>.</summary>
    public static explicit operator string(SqlStatement statement) => statement?.Minified();

    /// <summary>Represents the <see cref="SqlStatement"/> as a DEBUG <see cref="string"/>.</summary>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    internal string DebuggerDisplay => this.GetDebuggerDisplay();

    /// <summary>Converts a collection of objects to a collection of SQL statements.</summary>
    [Pure]
    internal static T[] ConvertAll<T>(object[] args, Func<object, T> converter) where T: ISqlStatement
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
    internal static ISqlStatement[] ConvertAll(params object[] args) 
        => ConvertAll<ISqlStatement>(args, (input) => (Literal)input.ToString());

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
    {
        return Convert<ISqlStatement>(arg, (input) => (Literal)input.ToString());
    }

    /// <summary>Trims null elements from the array.</summary>
    [Pure]
    internal static T[] Trim<T>(T[] args) => args is null ? new T[0] : args.Where(arg => !Equals(arg, default(T))).ToArray();
}
