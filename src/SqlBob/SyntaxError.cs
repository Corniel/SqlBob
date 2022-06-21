namespace SqlBob;

/// <summary>Represents a SQL Syntax error.</summary>
public class SyntaxError : SqlStatement
{
    /// <inherritdoc/>
    public SyntaxError(string message)
    {
        Message = Guard.NotNullOrEmpty(message, nameof(message));
    }

    /// <summary>The message that describes the SQL syntax error.</summary>
    public string Message { get; }
    
    /// <summary>Sets the <see cref="SqlBuilder.HasSyntaxError"/> to true with the message.</summary>
    public override void Write(SqlBuilder builder, int depth = 0)
    {
        Guard.NotNull(builder, nameof(builder));

        builder.HasSyntaxError = true;

        builder
            .Literal("/* ")
            .Literal(Message)
            .Literal(" */")
            .NewLine();
    }

    /// <summary>The JOIN expression ON condition has not been specified.</summary>
    public static SyntaxError JoinOnConditionNotSpecified => new SyntaxError(SqlBobMessages.SyntaxError_JoinOnConditionNotSpecified);

    /// <summary>The FROM expression has not been specified.</summary>
    public static SyntaxError FromExpressionNotSpecified => new SyntaxError(SqlBobMessages.SyntaxError_FromExpressionNotSpecified);

    /// <summary>Tries to parenthesize an empty SQL statement.</summary>
    public static SyntaxError ParenthesisIsEmpty => new SyntaxError(SqlBobMessages.SyntaxError_ParenthesisIsEmpty);
}
