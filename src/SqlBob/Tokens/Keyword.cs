using SqlBob.Formatting;
using System;
using System.Diagnostics;

namespace SqlBob
{
    /// <summary>Represent a SQL keyword token.</summary>
    [DebuggerDisplay("{DebuggerDisplay}")]
    public struct Keyword : ISqlStatement, IEquatable<Keyword>
    {
        /// <summary>ASC (ORDER BY expression ASC).</summary>
        public static readonly Keyword ASC = "ASC";

        /// <summary>AND (expression_0 AND expression_1).</summary>
        public static readonly Keyword AND = "AND";

        /// <summary>AS (expression AS alias).</summary>
        public static readonly Keyword AS = "AS";

        /// <summary>ASC (ORDER BY expression DESC).</summary>
        public static readonly Keyword DESC = "DESC";

        /// <summary>ONLY (OFFSET @skip ROWS FETCH NEXT @rows ROWS ONLY).</summary>
        public static readonly Keyword FETCH = "FETCH";

        /// <summary>GROUP BY (GROUP BY expression).</summary>
        public static readonly Keyword GROUP_BY = "GROUP BY";

        /// <summary>FROM (SELECT expression FROM table).</summary>
        public static readonly Keyword FROM = "FROM";

        /// <summary>NEXT (OFFSET @skip ROWS FETCH NEXT @rows ROWS ONLY).</summary>
        public static readonly Keyword NEXT = "NEXT";

        /// <summary>NOT (NOT expression).</summary>
        public static readonly Keyword NOT = "NOT";

        /// <summary>OFFSET (OFFSET @skip ROWS FETCH NEXT @rows ROWS ONLY).</summary>
        public static readonly Keyword OFFSET = "OFFSET";

        /// <summary>ON (JOIN table1 ON table0.column = table1.column).</summary>
        public static readonly Keyword ON = "ON";

        /// <summary>ONLY (OFFSET @skip ROWS FETCH NEXT @rows ROWS ONLY).</summary>
        public static readonly Keyword ONLY = "ONLY";

        /// <summary>OR (expression_0 OR expression_1).</summary>
        public static readonly Keyword OR = "OR";

        /// <summary>ORDER BY (ORDER BY expression).</summary>
        public static readonly Keyword ORDER_BY = "ORDER BY";

        /// <summary>ONLY (OFFSET @skip ROWS FETCH NEXT @rows ROWS ONLY).</summary>
        public static readonly Keyword ROWS = "ROWS";

        /// <summary>SELECT (SELECT expression).</summary>
        public static readonly Keyword SELECT = "SELECT";

        /// <summary>WHERE (WHERE expression).</summary>
        public static readonly Keyword WHERE = "WHERE";

        /// <summary>Creates a new instance of a <see cref="Keyword"/>.</summary>
        private Keyword(string value) => _value = value;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string _value;

        /// <summary>Implicitly casts a <see cref="string"/> to a <see cref="Keyword"/>.</summary>
        public static implicit operator Keyword(string value) => new Keyword(value);

        /// <inherritdoc/>
        public void Write(SqlBuilder builder, int depth = 0)
        {
            Guard.NotNull(builder, nameof(builder));

            var value = builder.FormatInfo.UseUpperCase
                ? _value
                : _value.ToLowerInvariant();
            builder.Literal(value);
        }

        /// <summary>Represents the <see cref="Keyword"/> as <see cref="string"/>.</summary>
        public override string ToString() => this.UseSqlBuilder();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => this.GetDebuggerDisplay();

        /// <inherritdoc/>
        public override bool Equals(object obj) => obj is Keyword other && Equals(other);

        /// <inherritdoc/>
        public bool Equals(Keyword other) => string.Equals(_value, other._value);

        /// <inherritdoc/>
        public override int GetHashCode() => _value is null ? 0 : _value.GetHashCode();
    }
}
