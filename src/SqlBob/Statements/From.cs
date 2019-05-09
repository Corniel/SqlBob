using SqlBob.Formatting;

namespace SqlBob
{
    /// <summary>Represents a SQL JOIN statement.</summary>
    public class From : SqlStatement
    {
        /// <summary>Creates a new instance of a <see cref="From"/> statement.</summary>
        public From(object expression)
        {
            Expression = Convert(expression) ?? SyntaxError.FromExpressionNotSpecified;
        }

        /// <summary>The table/select expression to select from.</summary>
        public ISqlStatement Expression { get; }

        /// <summary>The alias to refer to.</summary>
        public Alias Alias { get; private set; }

        /// <summary>Sets the AS alias.</summary>
        public From As(Alias alias)
        {
            Alias = alias;
            return this;
        }

        /// <inherritdoc/>
        public override void Write(SqlBuilder builder, int depth = 0)
        {
            Guard.NotNull(builder, nameof(builder));

            builder
                .Indent(depth)
                .Write(Keyword.FROM)
                .NewLineOrSpace()
                .Write(Expression, depth + 1);

            if (Alias.NotEmpty())
            {
                builder
                    .Space()
                    .Write(Keyword.AS)
                    .Space()
                    .Write(Alias);
            }
            builder.NewLineOrSpace();
        }
    }
}
