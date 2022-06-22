namespace SqlBob
{
    /// <summary>Represents a SQL INNER JOIN.</summary>
    public sealed record InnerJoin : Join<InnerJoin>
    {
        /// <inherritdoc/>
        internal InnerJoin(object epxression) : base(epxression) { }

        /// <inherritdoc/>
        public override Keyword JoinType => "INNER JOIN";
    }
}
