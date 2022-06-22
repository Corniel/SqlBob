namespace SqlBob
{
    /// <summary>Represents a SQL RIGHT (OUTER) JOIN.</summary>
    public sealed record RightJoin : Join<RightJoin>
    {
        /// <inherritdoc/>
        internal RightJoin(object expression) : base(expression) { }

        /// <inherritdoc/>
        public override Keyword JoinType => "RIGHT JOIN";
    }
}
