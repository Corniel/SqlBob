namespace SqlBob
{
    /// <summary>Represents a SQL RIGHT (OUTER) JOIN.</summary>
    public class RightJoin : Join<RightJoin>
    {
        /// <inherritdoc/>
        public RightJoin(object expression)
            : base(expression) { }

        /// <inherritdoc/>
        public override Keyword JoinType => "RIGHT JOIN";
    }
}
