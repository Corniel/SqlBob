namespace SqlBob;

/// <summary>Represents a SELECT query.</summary>
public class SelectQuery : SqlStatement
{
    /// <summary>The SELECT clause.</summary>
    public SelectClause SelectClause { get; internal init; }

    /// <summary>Gets the FROM clause.</summary>
    public From FromClause { get; private init; } = new(null);

    /// <summary>Gets the JOIN clause.</summary>
    public JoinClause JoinClause { get; private init; } = new();

    /// <summary>Gets the WHERE clause.</summary>
    public Where WhereClause { get; private init; } = SqlBob.Where.None;

    /// <summary>GETS the ORDER BY clause.</summary>
    public OrderByClause OrderByClause { get; private init; } = new();

    /// <summary>GETS the GROUP BY clause.</summary>
    public GroupByClause GroupByClause { get; private init; } = new();

    /// <summary>Extends the SELECT query with a FROM statement.</summary>
    [Pure]
    public SelectQuery From(object expression)
        => new()
        {
            SelectClause = SelectClause,
            FromClause = expression as From ?? new From(expression),
            JoinClause = JoinClause,
            WhereClause = WhereClause,
            OrderByClause = OrderByClause,
            GroupByClause = GroupByClause,
        };

    /// <summary>Extends the SELECT query with a FROM statement.</summary>
    [Pure]
    public SelectQuery From(object expression, Alias alias)
         => new()
         {
             SelectClause = SelectClause,
             FromClause = (expression as From ?? new From(expression)).As(alias),
             JoinClause = JoinClause,
             WhereClause = WhereClause,
             OrderByClause = OrderByClause,
             GroupByClause = GroupByClause,
         };
      
    /// <summary>Extends the SELECT query with JOIN statement(s).</summary>
    [Pure]
    public SelectQuery Join(params object[] expressions)
         => new()
         {
             SelectClause = SelectClause,
             FromClause = FromClause,
             JoinClause = Single<JoinClause>(expressions) ?? new(expressions),
             WhereClause = WhereClause,
             OrderByClause = OrderByClause,
             GroupByClause = GroupByClause,
         };


    /// <summary>Extends the SELECT query with a WHERE statement.</summary>
    [Pure]
    public SelectQuery Where(object expression)
        => new()
        {
            SelectClause = SelectClause,
            FromClause = FromClause,
            JoinClause = JoinClause,
            WhereClause = expression as Where ?? new Where(expression),
            OrderByClause = OrderByClause,
            GroupByClause = GroupByClause,
        };

    /// <summary>Extends the SELECT query with a ORDER BY statement.</summary>
    [Pure]
    public SelectQuery OrderBy(params object[] expressions)
         => new()
         {
             SelectClause = SelectClause,
             FromClause = FromClause,
             JoinClause = JoinClause,
             WhereClause = WhereClause,
             OrderByClause = Single<OrderByClause>(expressions) ?? new(expressions),
             GroupByClause = GroupByClause,
         };

    /// <summary>Extends the SELECT query with a GROUP BY statement.</summary>
    [Pure]
    public SelectQuery GroupBy(params object[] expressions)
         => new()
         {
             SelectClause = SelectClause,
             FromClause = FromClause,
             JoinClause = JoinClause,
             WhereClause = WhereClause,
             OrderByClause = OrderByClause,
             GroupByClause = Single<GroupByClause>(expressions) ?? new(expressions),
         };

    /// <inherritdoc/>
    public override void Write(SqlBuilder builder, int depth)
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

    [Pure]
    private static T Single<T>(object[] expressions) where T: class
        => expressions?.Length == 1
        ? expressions[0] as T
        : default;
}
