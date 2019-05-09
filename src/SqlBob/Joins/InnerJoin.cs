namespace SqlBob
{
    /// <summary>Represents a SQL INNER JOIN.</summary>
    public class InnerJoin : Join<InnerJoin>
    {
        /// <inherritdoc/>
        internal InnerJoin(object epxression)
            : base(epxression) { }

        /// <inherritdoc/>
        public override Keyword JoinType => "INNER JOIN";
    }
}
