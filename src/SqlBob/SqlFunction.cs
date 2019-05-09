using SqlBob.Formatting;

namespace SqlBob
{
    /// <summary>Represents a SQL function.</summary>
    public partial class SqlFunction : SqlStatement
    {
        /// <summary>Creates a new instance of a <see cref="SqlFunction"/>.</summary>
        internal SqlFunction(Literal name, params object[] arguments)
        {
            Name = name;
            Arguments = ConvertAll(arguments);
        }

        /// <summary>Gets the name of the SQL function.</summary>
        public Literal Name { get; }

        /// <summary>Gets the arguments of the SQL function.</summary>
        public ISqlStatement[] Arguments { get; }

        /// <inherritdoc/>
        public override void Write(SqlBuilder builder, int depth = 0)
        {
            Guard.NotNull(builder, nameof(builder));

            builder
                .Write(Name, depth)
                .Literal("(")
                .Join
                (
                    ", ",
                    (qb, statement) => qb.Write(statement),
                    Arguments
                )
                .Literal(")")
                .NewLineOrSpace()
            ;
        }
    }
}
