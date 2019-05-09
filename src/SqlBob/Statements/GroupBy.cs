using SqlBob.Formatting;
using System;

namespace SqlBob
{
    /// <summary>Represents a SQL GROUP BY statement.</summary>
    public class GroupBy : SqlStatement
    {
        /// <summary>Creates a new instance of an GROUP BY statement.</summary>
        public GroupBy(object expression)
        {
            Expression = Convert(expression);
        }

        /// <summary>Gets the expression to group by.</summary>
        public ISqlStatement Expression { get; }

        /// <inherritdoc/>
        public override void Write(SqlBuilder builder, int depth = 0)
        {
            Guard.NotNull(builder, nameof(builder));

            builder.Write(Expression, depth);
        }

        /// <summary>Implicitly casts a <see cref="string"/> to a <see cref="GroupBy"/>.</summary>
        public static implicit operator GroupBy(string value) => Parse(value);

        /// <summary>Parses the expression.</summary>
        public static GroupBy Parse(string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                return null;
            }

            return new GroupBy(expression);
        }
    }
}
