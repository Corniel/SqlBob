using SqlBob.Formatting;
using System;
using System.Diagnostics;
using System.Linq;

namespace SqlBob
{
    /// <summary>The default implementation of <see cref="ISqlStatement"/>.</summary>
    [DebuggerDisplay("{DebuggerDisplay}")]
    public abstract class SqlStatement : ISqlStatement
    {
        /// <inheritdoc/>
        public abstract void Write(SqlBuilder builder, int depth = 0);

        /// <inheritdoc/>
        public sealed override string ToString() => this.UseSqlBuilder();

        /// <summary>Explicitly cast a <see cref="SqlStatement"/> to a <see cref="string"/>.</summary>
        public static explicit operator string(SqlStatement statement) => statement?.Minified();

        /// <summary>Represents the <see cref="SqlStatement"/> as a DEBUG <see cref="string"/>.</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => this.GetDebuggerDisplay();

        /// <summary>Converts a collection of objects to a collection of SQL statements.</summary>
        internal static T[] ConvertAll<T>(object[] args, Func<object, T> converter) where T: ISqlStatement
        {
            if (args is null)
            {
                return new T[0];
            }

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

        /// <summary>Converts a collection of objects to a collection of SQL statements.</summary>
        internal static ISqlStatement[] ConvertAll(params object[] args)
        {
            return ConvertAll<ISqlStatement>(args, (input) => (Literal)input.ToString());
        }

        /// <summary>Converts an object to a SQL statement.</summary>
        internal static T Convert<T>(object arg, Func<object, T> converter) where T : ISqlStatement
        {
            if (arg is null || string.Empty.Equals(arg))
            {
                return default(T);
            }
            return (arg is T statement)
                ? statement
                : converter(arg);
        }

        /// <summary>Converts an object to a SQL statement.</summary>
        internal static ISqlStatement Convert(object arg)
        {
            return Convert<ISqlStatement>(arg, (input) => (Literal)input.ToString());
        }
        
        /// <summary>Trims null elements from the array.</summary>
        internal static T[] Trim<T>(T[] args) => args is null ? new T[0] : args.Where(arg => !Equals(arg, default(T))).ToArray();
    }
}
