namespace SqlBob;

public static class Token
{
    [Pure]
    public static Alias Alias(string alias) => alias;

    [Pure]
    public static Keyword Keyword(string keyword) => keyword;

    [Pure]
    public static Literal Literal(string literal) => literal;

    [Pure]
    public static Parameter Parameter(string parameter) => parameter;
}
