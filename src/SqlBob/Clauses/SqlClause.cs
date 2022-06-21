namespace SqlBob;

/// <summary>Represents a SQL clause with multiple child statements.</summary>
public abstract class SqlClause<T> : SqlStatement, IEnumerable<T>  where T : ISqlStatement
{
    /// <summary>Creates a new instance of a SQL clause.</summary>
    protected SqlClause(params object[] expressions)
    {
        _expressions.AddRange(ConvertAll(expressions, Cast));
    }

    /// <summary>Gets the number of statements of the clause.</summary>
    public int Count => _expressions.Count;

    /// <summary>Adds (not null) expressions to the clause.</summary>
    public void Add(T expression)
    {
        var casted = Cast(expression);

        if (Equals(casted ,default(T)))
        {
            _expressions.Add(casted);
        }
    }

    /// <summary>Casts a (not null) object to type of the clause.</summary>
    [Pure]
    protected abstract T Cast(object arg);

    /// <inherritdoc/>
    [Pure]
    public IEnumerator<T> GetEnumerator() => _expressions.GetEnumerator();

    /// <inherritdoc/>
    [Pure]
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>The underlying list.</summary>
    private readonly List<T> _expressions = new();
}
