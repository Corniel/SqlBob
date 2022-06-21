namespace SqlBob;

public partial class SqlFunction
{
    /// <summary>Returns a Count(*) SQL function.</summary>
    [Pure]
    public static SqlFunction Count() => new(nameof(Count), "*");

    /// <summary>Returns a GetDate() SQL function.</summary>
    [Pure]
    public static SqlFunction GetDate() => new(nameof(GetDate));

    /// <summary>Returns a GetUtcDate() SQL function.</summary>
    [Pure]
    public static SqlFunction GetUtcDate() => new(nameof(GetUtcDate));
}
