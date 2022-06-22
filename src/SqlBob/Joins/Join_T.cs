namespace SqlBob;

/// <summary>Represents a SQL JOIN.</summary>
/// <typeparam name="T">
/// The type of the join.
/// </typeparam>
public abstract record Join<T> : Join where T: Join<T>
{
    /// <inherritdoc/>
    protected Join(object expression) 
        : base(expression) { }

    /// <summary>Sets the AS alias.</summary>
    [Pure]
    public T As(Alias alias) => (T)this with { Alias = alias };
    
    /// <summary>Sets the ON condition</summary>
    [Pure]
    public T On(object condition) 
        => (T) this with
        {
            Condition = SQL.Convert(condition) ?? SyntaxError.JoinOnConditionNotSpecified 
        };
}
