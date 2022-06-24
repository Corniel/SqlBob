namespace SqlBob;

public sealed class Query : SqlStatement
{
    public static Query Select(params object[] selections)
        => new(
            select: SqlStatements.None.AddRange(SQL.ConvertAll(selections)),
            from: SQL.Missing("from statement"),
            join: SqlStatements.None,
            where: SQL.None,
            orderBy: SqlStatements.None);

    internal Query(
        SqlStatements select, 
        SqlStatement from,
        SqlStatements join,
        SqlStatement where,
        SqlStatements orderBy)
    {
        SelectClause = select;
        FromClause = from;
        JoinClause = join;
        WhereClause = where;
        OrderByClause = orderBy;
    }

    public SqlStatements SelectClause { get; }
    public SqlStatement FromClause { get; }
    public SqlStatements JoinClause { get; }
    public SqlStatement WhereClause { get; }
    public SqlStatements OrderByClause { get; }

    [Pure]
    public Query From(object expression)
        => new(
            select: SelectClause,
            from: SQL.Convert(expression) ?? SQL.Missing("from statement"),
            join: JoinClause,
            where: WhereClause, 
            orderBy: OrderByClause);

    [Pure]
    public Query Join(params object[] join)
      => new(
          select: SelectClause,
          from: FromClause,
          join: SqlStatements.None.AddRange(SQL.ConvertAll(join)),
          where: WhereClause,
          orderBy: OrderByClause);

    [Pure]
    public Query Where(object expression)
        => new(
            select: SelectClause, 
            from: FromClause, 
            join: JoinClause,
            where: SQL.Convert(expression) ?? SQL.None,
            orderBy: OrderByClause);

    [Pure]
    public Query OrderBy(params object[] orderBy)
        => new(
            select: SelectClause, 
            from: FromClause,
            join: JoinClause,
            where: WhereClause,
            orderBy: SqlStatements.None.AddRange(SQL.ConvertAll(orderBy)));

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
        if (WhereClause is not None)
        {
            builder
                .Indent(depth)
                .Write(Keyword.WHERE)
                .NewLineOrSpace()
                .Write(WhereClause, depth + 1)
                .NewLineOrSpace();
        }
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
    }
}
