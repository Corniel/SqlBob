using SqlBob.Formatting;

namespace SqlBob
{
    /// <summary>Represents a clause of JOIN statements.</summary>
    public class JoinClause : SqlClause<Join>
    {
        /// <summary>Creates a new instance of a <see cref="JoinClause"/>.</summary>
        public JoinClause(params object[] expressions)
            : base(expressions) { }

        /// <inherritdoc/>
        public override void Write(SqlBuilder builder, int depth = 0)
        {
            foreach(var join in this)
            {
                join.Write(builder, depth);
            }
        }

        /// <inherritdoc/>
        protected override Join Cast(object arg) => arg.ToString();
    }
}
