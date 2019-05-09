using SqlBob.Formatting;
using System;

namespace SqlBob
{
    /// <summary>Represents a SQL ORDER BY statement.</summary>
    public class OrderBy : SqlStatement
    {
        /// <summary>Creates a new instance of an ORDER BY statement.</summary>
        public OrderBy(object expression, bool desc)
        {
            Expression = Convert(expression);
            IsDesc = desc;
        }

        /// <summary>Gets the expression to order by.</summary>
        public ISqlStatement Expression { get; }

        /// <summary>Descending or ascending.</summary>
        public bool IsDesc { get; }

        /// <inherritdoc/>
        public override void Write(SqlBuilder builder, int depth = 0)
        {
            Guard.NotNull(builder, nameof(builder));

            builder
                .Write(Expression, depth)
                .Space()
                .Write(IsDesc ? Keyword.DESC : Keyword.ASC)
            ;
        }

        /// <summary>Implicitly casts a <see cref="string"/> to a <see cref="OrderBy"/>.</summary>
        public static implicit operator OrderBy(string value) => Parse(value);

        /// <summary>Parses the expression.</summary>
        public static OrderBy Parse(string expression)
        {
            if(string.IsNullOrEmpty(expression))
            {
                return null;
            }
            if(expression.EndsWith(" ASC", StringComparison.InvariantCultureIgnoreCase))
            {
                return new OrderBy(expression.Substring(0, expression.Length - 4).Trim(), false);
            }
            if (expression.EndsWith(" DESC", StringComparison.InvariantCultureIgnoreCase))
            {
                return new OrderBy(expression.Substring(0, expression.Length - 5).Trim(), true);
            }
            return Asc(expression);
        }

        /// <summary>ORDER BY expression ASC.</summary>
        public static OrderBy Asc(object expression) => new OrderBy(expression, false);

        /// <summary>ORDER BY expression DESC.</summary>
        public static OrderBy Desc(object expression) => new OrderBy(expression, true);
    }
}
