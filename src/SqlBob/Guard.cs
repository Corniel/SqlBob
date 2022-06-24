namespace SqlBob;

internal static class Guard
{
    [DebuggerStepThrough]
    public static T NotNull<T>(T? parameter, string paramName) where T : class
        => parameter ?? throw new ArgumentNullException(paramName);

    [DebuggerStepThrough]
    public static string NotNullOrEmpty([ValidatedNotNull] string? parameter, string paramName)
        => NotNull(parameter, paramName) is { Length: > 0 } str
        ? str
        : throw new ArgumentException("Argument cannot be an empty string.", paramName);

    [AttributeUsage(AttributeTargets.Parameter)]
    sealed class ValidatedNotNullAttribute : Attribute { }
}
