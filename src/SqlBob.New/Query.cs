namespace SqlBob;

public sealed record Query : SqlStatement
{
    public static Query Select(params object[] selections)
        => new(
            SqlStatements.None.AddRange(SQL.ConvertAll(selections)),
            SQL.Missing("from statement"),
            SQL.None,
            SqlStatements.None);

    internal Query(
        SqlStatements selections, 
        SqlStatement from,
        SqlStatement where,
        SqlStatements orderBy)
    {
        SelectClause = selections;
        FromClause = from;
        WhereClause = where;
        OrderByClause = orderBy;
    }

    public SqlStatements SelectClause { get; }
    public SqlStatement FromClause { get; }
    public SqlStatement WhereClause { get; }
    public SqlStatements OrderByClause { get; }

    [Pure]
    public Query From(object expression)
        => new(SelectClause, SQL.Convert(expression) ?? SQL.Missing("from statement"), WhereClause, OrderByClause);

    [Pure]
    public Query Where(object expression)
        => new(SelectClause, FromClause, SQL.Convert(expression) ?? SQL.None, OrderByClause);

    [Pure]
    public Query OrderBy(params object[] orderBys)
        => new(SelectClause, FromClause, WhereClause, SqlStatements.None.AddRange(SQL.ConvertAll(orderBys)));

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
