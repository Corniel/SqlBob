namespace SqlBob;

public partial record Join
{
    /// <summary>Creates a JOIN based on a raw SQL <see cref="string"/>.</summary>
    [Pure]
    public static Join Raw(string value) => value;

    /// <summary>Creates an INNER JOIN.</summary>
    [Pure]
    public static InnerJoin Inner(object expression) => new(expression);

    /// <summary>Creates an LEFT (OUTER) JOIN.</summary>
    [Pure]
    public static LeftJoin Left(object expression) => new(expression);

    /// <summary>Creates an RIGHT (OUTER) JOIN.</summary>
    [Pure]
    public static RightJoin Right(object expression) => new(expression);

    /// <summary>Creates an FULL OUTER JOIN.</summary>
    [Pure]
    public static FullOutherJoin FullOuther(object expression) => new(expression);
}
