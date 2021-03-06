﻿using SqlBob.Formatting;

namespace SqlBob
{
    /// <summary>Represent a SQL NOT statement.</summary>
    public class Not : Logical
    {
        internal Not(object expression) : base(expression) { }

        /// <summary>The NOT operator keyword.</summary>
        public override Keyword Operator => Keyword.NOT;

        /// <inheritdoc/>
        public override void Write(SqlBuilder builder, int depth = 0)
        {
            Guard.NotNull(builder, nameof(builder));

            builder
                .Write(Operator, depth)
                .Literal("(")
                .NewLine()
                .Write(this[0], depth + 1)
                .NewLine()
                .Indent(depth)
                .Literal(")")
                .NewLine()
            ;
        }
    }
}
