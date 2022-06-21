namespace SqlBob;

/// <summary>Represents a SELECT query.</summary>
public class SelectQuery : SqlStatement
{
    /// <summary>The SELECT clause.</summary>
    public SelectClause SelectClause { get; internal init; }

    /// <summary>Gets the FROM clause.</summary>
    public From FromClause { get; private set; } = new From(null);

    /// <summary>Gets the JOIN clause.</summary>
    public JoinClause JoinClause { get; private set; } = new JoinClause();

    /// <summary>Gets the WHERE clause.</summary>
    public Where WhereClause { get; private set; } = SqlBob.Where.None;

    /// <summary>GETS the ORDER BY clause.</summary>
    public OrderByClause OrderByClause { get; private set; } = new OrderByClause();

    /// <summary>GETS the GROUP BY clause.</summary>
    public GroupByClause GroupByClause { get; private set; } = new GroupByClause();

    /// <summary>Extends the SELECT query with a FROM statement.</summary>
    [Impure]
    public SelectQuery From(object expression)
    {
        FromClause = expression as From ?? new From(expression);
        return this;
    }

    /// <summary>Extends the SELECT query with a FROM statement.</summary>
    [Impure]
    public SelectQuery From(object expression, Alias alias)
    {
        From(expression);
        FromClause = FromClause.As(alias);
        return this;
    }

    /// <summary>Extends the SELECT query with JOIN statement(s).</summary>
    [Impure]
    public SelectQuery Join(params object[] expressions)
    {
        JoinClause =
        (
            expressions != null &&
            expressions.Length == 1 &&
            expressions[0] is JoinClause clause
        )
            ? clause
            : new JoinClause(expressions);

        return this;
    }

    /// <summary>Extends the SELECT query with a WHERE statement.</summary>
    [Impure]
    public SelectQuery Where(object expression)
    {
        WhereClause = expression as Where ?? new Where(expression);
        return this;
    }

    /// <summary>Extends the SELECT query with a ORDER BY statement.</summary>
    [Impure]
    public SelectQuery OrderBy(params object[] expressions)
    {
        OrderByClause =
        (
            expressions != null &&
            expressions.Length == 1 &&
            expressions[0] is OrderByClause clause
        )
            ? clause
            : new OrderByClause(expressions);

        return this;
    }

    /// <summary>Extends the SELECT query with a GROUP BY statement.</summary>
    [Impure]
    public SelectQuery GroupBy(params object[] expressions)
    {
        GroupByClause =
        (
            expressions != null &&
            expressions.Length == 1 &&
            expressions[0] is GroupByClause clause
        )
            ? clause
            : new GroupByClause(expressions);

        return this;

    }



    /// <inherritdoc/>
    public override void Write(SqlBuilder builder, int depth = 0)
    {
        Guard.NotNull(builder, nameof(builder));

        builder
            .Write(SelectClause).NewLineOrSpace()
            .Write(FromClause).NewLineOrSpace()
            .Write(JoinClause).NewLineOrSpace()
            .Write(WhereClause).NewLineOrSpace()
            .Write(GroupByClause).NewLineOrSpace()
            .Write(OrderByClause).NewLineOrSpace()
        ;
    }
}
