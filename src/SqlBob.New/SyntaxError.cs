namespace SqlBob;

public sealed record SyntaxError : SqlStatement
{
    internal SyntaxError(string message) => Message = message;
    
    /// <summary>The message that describes the SQL syntax error.</summary>
    public string Message { get; }

    /// <summary>Sets the <see cref="SqlBuilder.HasSyntaxError"/> to true with the message.</summary>
    public override void Write(SqlBuilder builder, int depth)
    {
        Guard.NotNull(builder, nameof(builder));

        builder.HasSyntaxError = true;

        builder
            .Literal("/* ")
            .Literal(Message)
            .Literal(" */")
            .NewLine();
    }
}
