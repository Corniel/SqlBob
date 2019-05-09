namespace SqlBob
{
    /// <summary>Represents a SQL LEFT (OUTER) JOIN.</summary>
    public class LeftJoin : Join<LeftJoin>
    {
        /// <inherritdoc/>
        internal LeftJoin(object expression)
            : base(expression) { }

        /// <inherritdoc/>
        public override Keyword JoinType => "LEFT JOIN";
    }
}
