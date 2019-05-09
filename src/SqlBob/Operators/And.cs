namespace SqlBob
{
    /// <summary>Represent a SQL AND statement.</summary>
    public class And : Logical
    {
        internal And(params object[] expressions) : base(expressions) { }

        /// <summary>The AND operator keyword.</summary>
        public override Keyword Operator => Keyword.AND;
    }
}
