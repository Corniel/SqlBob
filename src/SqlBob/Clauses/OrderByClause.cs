using SqlBob.Formatting;
using System.Linq;

namespace SqlBob
{
    /// <summary>Represents the ORDER BY clause.</summary>
    public class OrderByClause : SqlClause<OrderBy>
    {
        /// <summary>Creates a new instance of an ORDER BY clause.</summary>
        public OrderByClause(params object[] expressions)
            : base(expressions) { }

        /// <summary>Gets the OFFSET skip.</summary>
        public ISqlStatement Skip { get; private set; }

        /// <summary>Gets the FETCH NEXT row count.</summary>
        public ISqlStatement RowCount { get; private set; }

        /// <summary>Set the OFFSET.</summary>
        public OrderByClause Offset(object skip)
        {
            Skip = Convert(skip);
            return this;
        }

        /// <summary>Set the FETCH NEXT.</summary>
        public OrderByClause FetchNext(object rowCount)
        {
            RowCount = Convert(rowCount);
            return this;
        }

        /// <inherritdoc/>
        public override void Write(SqlBuilder builder, int depth = 0)
        {
            Guard.NotNull(builder, nameof(builder));

            // If empty don't do anything.
            if (Count == 0)
            {
                return;
            }

            builder.Write(Keyword.ORDER_BY, depth)
                .Join
                (
                    ",",
                    (qb, statement) => qb.NewLineOrSpace().Write(statement, depth + 1),
                    this.ToArray()
                )
                .NewLineOrSpace()
            ;

            if (Skip != null)
            {
                builder
                   .Indent(depth)
                   .Write(Keyword.OFFSET)
                   .Space()
                   .Write(Skip)
                   .Space()
                   .Write(Keyword.ROWS);
            }
            if (RowCount != null)
            {
                builder
                   .Space()
                   .Write(Keyword.FETCH)
                   .Space()
                   .Write(Keyword.NEXT)
                   .Space()
                   .Write(RowCount)
                   .Space()
                   .Write(Keyword.ROWS)
                   .Space()
                   .Write(Keyword.ONLY);
            }
        }

        /// <inherritdoc/>
        protected override OrderBy Cast(object arg) => arg.ToString();
    }
}
