namespace SqlBob;

public sealed class Query : SqlStatement
{
    internal static readonly Query Empty = new(null, null, null, null, null, null, null);

    internal Query(
        SqlStatement? select, 
        SqlStatement? from,
        SqlStatements? join,
        SqlStatement? where,
        SqlStatement? orderBy,
        SqlStatement? offset,
        SqlStatement? fetch)
    {
        SelectClause = select.Required(nameof(select));
        FromClause = from.Required(nameof(from));
        JoinClause = join.Optional();
        WhereClause = where.Optional();
        OrderByClause = orderBy.Optional();
        OffsetClause = offset.Optional();
        FetchClause = fetch.Optional();
    }

    public SqlStatement SelectClause { get; }
    public SqlStatement FromClause { get; }
    public SqlStatements JoinClause { get; }
    public SqlStatement WhereClause { get; }
    public SqlStatement OrderByClause { get; }
    public SqlStatement OffsetClause { get; }
    public SqlStatement FetchClause { get; }

    [Pure]
    public Query Select(params object[] selections)
        => new(
        select: SqlBob.Select.Convert(selections),
        from: FromClause,
        join: JoinClause,
        where: WhereClause,
        orderBy: OrderByClause,
        offset: OffsetClause,
        fetch: FetchClause);

    [Pure]
    public Query From(object? from)
        => new(
        select: SelectClause,
        from: Convert(from, x => new From(x)),
        join: JoinClause,
        where: WhereClause, 
        orderBy: OrderByClause,
        offset: OffsetClause,
        fetch: FetchClause);

    [Pure]
    public Query Join(params object[] join)
        => new(
        select: SelectClause,
        from: FromClause,
        join: SqlStatements.None.AddRange(SQL.ConvertAll(join)),
        where: WhereClause,
        orderBy: OrderByClause,
        offset: OffsetClause,
        fetch: FetchClause);

    [Pure]
    public Query Where(object? where)
        => new(
        select: SelectClause, 
        from: FromClause, 
        join: JoinClause,
        where: Convert(where, x => new Where(x)),
        orderBy: OrderByClause,
        offset: OffsetClause,
        fetch: FetchClause);

    [Pure]
    public Query OrderBy(params object[] orderBy)
        => new(
        select: SelectClause, 
        from: FromClause,
        join: JoinClause,
        where: WhereClause,
        orderBy: SqlBob.OrderBy.Convert(orderBy),
        offset: OffsetClause,
        fetch: FetchClause);

    [Pure]
    public Query Offset(object? offset)
        => new(
        select: SelectClause,
        from: FromClause,
        join: JoinClause,
        where: WhereClause,
        orderBy: OrderByClause.Required("orderBy"),
        offset: Convert(offset, x => new Offset(x)),
        fetch: FetchClause);

    [Pure]
    public Query Fetch(object? fetch)
        => new(
        select: SelectClause,
        from: FromClause,
        join: JoinClause,
        where: WhereClause,
        orderBy: OrderByClause.Required("orderBy"),
        offset: OffsetClause,
        fetch: Convert(fetch, x => new Fetch(x)));

    public override void Write(SqlBuilder builder, int depth)
        => Guard.NotNull(builder, nameof(builder))
        .Write(SelectClause, depth)
        .Write(FromClause, depth)
        .Write(WhereClause, depth + 1)
        .Write(JoinClause, depth)
        .Write(OrderByClause, depth)
        .Write(OffsetClause, depth)
        .Write(FetchClause, depth)
        .NewLineOrSpace();
}
