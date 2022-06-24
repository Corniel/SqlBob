namespace SqlBob;

public abstract class Join : SqlStatement
{
    /// <summary>Creates an INNER JOIN.</summary>
    [Pure]
    public static InnerJoin Inner(object table) => new(SQL.Convert(table), null);

    /// <summary>Creates an LEFT (OUTER) JOIN.</summary>
    [Pure]
    public static LeftJoin Left(object table) => new(SQL.Convert(table), null);

    /// <summary>Creates an RIGHT (OUTER) JOIN.</summary>
    [Pure]
    public static RightJoin Right(object table) => new(SQL.Convert(table), null);

    /// <summary>Creates an FULL OUTER JOIN.</summary>
    [Pure]
    public static FullOutherJoin FullOuther(object table) => new(SQL.Convert(table), null);

    protected Join(SqlStatement? table, SqlStatement? condition)
    {
        Table = table ?? SQL.Missing("table");
        Condition = condition ?? SQL.Missing("condition");
    }

    protected abstract Keyword JoinType { get; }

    public SqlStatement Table { get; }

    public SqlStatement Condition { get; }

    /// <inherritdoc/>
    public override void Write(SqlBuilder builder, int depth)
        => Guard.NotNull(builder, nameof(builder))
        .Indent(depth)
        .Write(JoinType)
        .NewLineOrSpace()
        .Indent(depth + 1)
        .Write(Table)
        .Space().Write(Keyword.ON).Space()
        .Write(Condition)
        .NewLineOrSpace();
}
