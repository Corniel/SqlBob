﻿namespace SqlBob;

internal static class Guarding
{
    private static readonly None Nill = new();

    [Pure]
    public static SqlStatement Optional(this SqlStatement? statement)
        => statement ?? Nill;

    [Pure]
    public static SqlStatement Required(this SqlStatement? statement, string message)
        => statement is not null && statement is not None
        ? statement
        : new Missing(message);

    private sealed class None : SqlStatement
    {
        [Pure]
        public override SqlStatement Not() => this.Required("expression");

        [Pure]
        public override Selection As(Alias alias) => new(null, alias);

        public override void Write(SqlBuilder builder, int depth) => Guard.NotNull(builder, nameof(builder));
    }

    private sealed class Missing : SqlStatement
    {
        internal Missing(string message) => Message = message;

        /// <summary>The message that describes the SQL syntax error.</summary>
        public string Message { get; }

        /// <summary>Sets the <see cref="SqlBuilder.HasSyntaxError"/> to true with the message.</summary>
        public override void Write(SqlBuilder builder, int depth)
        {
            Guard.NotNull(builder, nameof(builder))
                .Literal("/* missing ")
                .Literal(Message)
                .Literal(" */")
                .NewLineOrSpace();

            builder.HasSyntaxError = true;
        }
    }
}
