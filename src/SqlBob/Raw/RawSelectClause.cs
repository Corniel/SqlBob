using SqlBob.Formatting;
using System.Diagnostics;

namespace SqlBob
{
    /// <summary>To support a custom <see cref="SelectClause"/> based on a <see cref="string"/>.</summary>
    internal class RawSelectClause : SelectClause
    {
        /// <summary>Creates a new instance of a raw <see cref="SelectClause"/>.</summary>
        public RawSelectClause(string raw) => _raw = raw;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string _raw;

        /// <inherritdoc/>
        public override void Write(SqlBuilder builder, int depth = 0)
        {
            Guard.NotNull(builder, nameof(builder));

            builder.Literal(_raw).NewLineOrSpace();
        }
    }
}

