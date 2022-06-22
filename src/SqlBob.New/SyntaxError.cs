namespace SqlBob;

[Serializable]
public class SyntaxError : FormatException
{
    public SyntaxError() { }

    public SyntaxError(string? message) : base(message) { }

    public SyntaxError(string? message, Exception? innerException) : base(message, innerException) { }

    protected SyntaxError(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
