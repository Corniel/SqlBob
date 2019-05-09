using SqlBob.Formatting;

namespace SqlBob
{
    internal class LogicalExpression : Logical
    {
        internal LogicalExpression(string @operator, object left, object right)
            : base(new[] 
            {
                Guard.NotNull(left, nameof(left)),
                Guard.NotNull(right, nameof(right)),
            })
        {
            Operator = @operator;
        }

        public override Keyword Operator { get; }

        public override void Write(SqlBuilder builder, int depth = 0)
        {
            Guard.NotNull(builder, nameof(builder));

            builder
                .Write(this[0], depth)
                .NewLineOrSpace()
                .Write(Operator, depth)
                .NewLineOrSpace()
                .Write(this[1], depth)
                .NewLine();
        }
    }
}
