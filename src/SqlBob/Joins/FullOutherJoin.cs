namespace SqlBob
{
    /// <summary>Represents a SQL FULL OUTER JOIN.</summary>
    public class FullOutherJoin : Join<FullOutherJoin>
    {
        /// <inherritdoc/>
        public FullOutherJoin(object expression)
            : base(expression) { }

        /// <inherritdoc/>
        public override Keyword JoinType => "FULL OUTHER JOIN";
    }
}
