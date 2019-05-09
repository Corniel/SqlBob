using SqlBob.Formatting;

namespace SqlBob
{
    /// <summary>Represents a set of parenthesis around a SQL expression.</summary>
    public class Parenthesis : SqlStatement
    {
        /// <summary>Creates a new instance of a <see cref="Parenthesis"/>.</summary>
        public Parenthesis(object expression)
        {
            Expression = Convert(expression) ?? SyntaxError.ParenthesisIsEmpty;
        }

        /// <summary>The SQL expression to parenthesize.</summary>
        public ISqlStatement Expression { get; }

        /// <inheritdoc />
        public override void Write(SqlBuilder builder, int depth = 0)
        {
            Guard.NotNull(builder, nameof(builder));

            builder
                .Indent(depth)
                .Literal("(")
                .NewLine()
                .Write(Expression, depth + 1)
                .NewLine()
                .Indent(depth)
                .Literal(")")
                .NewLineOrSpace();
        }
    }
}
