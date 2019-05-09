namespace SqlBob
{
    /// <summary>Represent a SQL OR statement.</summary>
    public class Or : Logical
    {
        internal Or(params object[] expressions) : base(expressions) { }

        /// <summary>The OR operator keyword.</summary>
        public override Keyword Operator => Keyword.OR;
    }
}
