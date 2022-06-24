namespace SqlBob;

public sealed class Query : SqlStatement
{
    internal static readonly Query Empty = new(SqlStatements.None, null, SqlStatements.None, null, SqlStatements.None, null, null);

    internal Query(
        SqlStatements select, 
        SqlStatement? from,
        SqlStatements join,
        SqlStatement? where,
        SqlStatements orderBy,
        SqlStatement? offset,
        SqlStatement? fetch)
    {
        SelectClause = select;
        FromClause = from.Required(nameof(from));
        JoinClause = join;
        WhereClause = where.Optional();
        OrderByClause = orderBy;
        OffsetClause = offset.Optional();
        FetchClause = fetch.Optional();
    }

    public SqlStatements SelectClause { get; }
    public SqlStatement FromClause { get; }
    public SqlStatements JoinClause { get; }
    public SqlStatement WhereClause { get; }
    public SqlStatements OrderByClause { get; }
    public SqlStatement OffsetClause { get; }
    public SqlStatement FetchClause { get; }

    [Pure]
    public Query Select(params object[] selections)
        => new(
        select: SqlStatements.None.AddRange(SQL.ConvertAll(selections)),
        from: FromClause,
        join: JoinClause,
        where: WhereClause,
        orderBy: OrderByClause,
        offset: OffsetClause,
        fetch: FetchClause);

    [Pure]
    public Query From(object? expression)
        => new(
        select: SelectClause,
        from: SQL.Convert(expression).Required("from"),
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
        orderBy: SqlStatements.None.AddRange(SQL.ConvertAll(orderBy)),
        offset: OffsetClause,
        fetch: FetchClause);

    [Pure]
    public Query Offset(object? offset)
        => new(
        select: SelectClause,
        from: FromClause,
        join: JoinClause,
        where: WhereClause,
        orderBy: OrderByClause, // TODO: required
        offset: Convert(offset, x => new Offset(x)),
        fetch: FetchClause);

    [Pure]
    public Query Fetch(object? fetch)
        => new(
        select: SelectClause,
        from: FromClause,
        join: JoinClause,
        where: WhereClause,
        orderBy: OrderByClause, // TODO: required
        offset: OffsetClause,
        fetch: Convert(fetch, x => new Fetch(x)));

    public override void Write(SqlBuilder builder, int depth)
    {
        Guard.NotNull(builder, nameof(builder)).Indent(depth);
        builder
            .Indent(depth)
            .Write(Keyword.SELECT)
            .Join(
                ",",
                (qb, select) => qb.NewLineOrSpace().Indent(depth + 1).Write(select, 0),
                SelectClause)
            .NewLineOrSpace()
            .Indent(depth)
            .Write(Keyword.FROM)
            .Space()
            .Write(FromClause, 0)
            .NewLineOrSpace();

        foreach(var join in JoinClause)
        {
            builder.Write(join, depth);
        }
        builder.Write(WhereClause, depth + 1);
        if (OrderByClause.Any())
        {
            builder.Indent(depth)
                .Write(Keyword.ORDER_BY)
                .NewLineOrSpace()
                .Join(
                    ",",
                    (qb, statement) => qb.NewLineOrSpace().Write(statement, depth + 1),
                    OrderByClause
                );
        }
        builder.Write(OffsetClause);
    }
}
