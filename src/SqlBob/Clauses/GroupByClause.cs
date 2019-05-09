using SqlBob.Formatting;
using System.Linq;

namespace SqlBob
{
    /// <summary>Represents the GROUP BY clause.</summary>
    public class GroupByClause : SqlClause<GroupBy>
    {
        /// <summary>Creates a new instance of an ORDER BY clause.</summary>
        public GroupByClause(params object[] expressions)
            : base(expressions) { }

        /// <inherritdoc/>
        public override void Write(SqlBuilder builder, int depth = 0)
        {
            Guard.NotNull(builder, nameof(builder));

            // If empty don't do anything.
            if (Count == 0)
            {
                return;
            }

            builder.Write(Keyword.GROUP_BY, depth)
                .Join
                (
                    ",",
                    (qb, statement) => qb.NewLineOrSpace().Write(statement, depth + 1),
                    this.ToArray()
                )
                .NewLineOrSpace()
            ;
        }

        /// <inherritdoc/>
        protected override GroupBy Cast(object arg) => arg.ToString();
    }
}
