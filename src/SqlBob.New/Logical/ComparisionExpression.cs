namespace SqlBob
{
    public sealed class ComparisionExpression : SqlStatement
    {
        internal ComparisionExpression(SqlStatement left, Keyword @operator, SqlStatement right)
        {
            Operator = @operator;
            Left = left;
            Right = right;
        }

        public Keyword Operator { get; }
        public SqlStatement Left { get; }
        public SqlStatement Right { get; }

        public override void Write(SqlBuilder builder, int depth)
            => Guard.NotNull(builder, nameof(builder))
            .Write(Left, depth)
            .Space().Write(Operator)
            .Space()
            .Write(Right, depth)
            .NewLineOrSpace()
        ;
    }
}
