using SqlBob.Formatting;

namespace SqlBob
{
    /// <summary>To support custom <see cref="Join"/>s based on a <see cref="string"/>.</summary>
    internal class RawJoin : Join
    {
        /// <summary>Creates a new instance of a raw <see cref="Join"/>.</summary>
        public RawJoin(Literal raw) : base(raw) { }

        /// <inherritdoc/>
        public override Keyword JoinType => default(Keyword);

        /// <inherritdoc/>
        public override void Write(SqlBuilder builder, int depth = 0)
        {
            Guard.NotNull(builder, nameof(builder));
            builder
                .Indent(depth)
                .Write(Expression, depth)
                .NewLineOrSpace();
        }
    }
}
