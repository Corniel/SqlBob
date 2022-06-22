using SqlBob.Formatting;

namespace SqlBob
{
    /// <summary>Represents a SQL WHERE statement.</summary>
    public class Where : SqlStatement
    {
        /// <summary>An empty/not set WHERE statement.</summary>
        public static readonly Where None = new Where(null);

        /// <summary>Creates a new instance of a <see cref="Where"/> statement.</summary>
        public Where(object expression) => Condition = Convert(expression);

        /// <summary>The WHERE condition to apply.</summary>
        public ISqlStatement Condition { get; }

        /// <inherritdoc/>
        public override void Write(SqlBuilder builder, int depth)
        {
            Guard.NotNull(builder, nameof(builder));

            if (Condition is null)
            {
                if (builder.IsDebug)
                {
                    builder.Literal("/* No WHERE statement */");
                }
            }
            else
            {
                builder
                    .Indent(depth)
                    .Write(Keyword.WHERE)
                    .NewLineOrSpace()
                    .Write(Condition, depth + 1)
                    .NewLineOrSpace()
                ;
            }
        }
    }
}
