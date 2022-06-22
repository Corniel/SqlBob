namespace SqlBob;

internal sealed record MissingSql : SqlStatement
{
    internal MissingSql(string message) => Message = message;
    
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
