namespace SqlBob
{
    /// <summary>Represents a SQL FULL OUTER JOIN.</summary>
    public sealed record FullOutherJoin : Join<FullOutherJoin>
    {
        /// <inherritdoc/>
        internal FullOutherJoin(object expression) : base(expression) { }

        /// <inherritdoc/>
        public override Keyword JoinType => "FULL OUTHER JOIN";
    }
}
