namespace SqlBob;

public abstract class Join : SqlStatement
{
    /// <summary>Creates an INNER JOIN.</summary>
    [Pure]
    public static InnerJoin Inner(object expression) => new(SQL.Convert(expression), null);

    /// <summary>Creates an LEFT (OUTER) JOIN.</summary>
    [Pure]
    public static LeftJoin Left(object expression) => new(SQL.Convert(expression), null);

    /// <summary>Creates an RIGHT (OUTER) JOIN.</summary>
    [Pure]
    public static RightJoin Right(object expression) => new(SQL.Convert(expression), null);

    /// <summary>Creates an FULL OUTER JOIN.</summary>
    [Pure]
    public static FullOutherJoin FullOuther(object expression) => new(SQL.Convert(expression), null);

    protected Join(SqlStatement? expression, SqlStatement? condition)
    {
        Expression = expression ?? SQL.Missing("join");
        Condition = condition ?? SQL.Missing("join condition");
    }

    protected abstract Keyword JoinType { get; }

    public SqlStatement Expression { get; }

    public SqlStatement Condition { get; }

    /// <inherritdoc/>
    public override void Write(SqlBuilder builder, int depth)
        => Guard.NotNull(builder, nameof(builder))
        .Indent(depth)
        .Write(JoinType)
        .NewLineOrSpace()
        .Indent(depth + 1)
        .Write(Expression)
        .Space().Write(Keyword.ON).Space()
        .Write(Condition)
        .NewLineOrSpace();
}
