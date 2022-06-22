namespace SqlBob;

/// <summary>Represents a SQL JOIN.</summary>
public abstract partial record Join : ISqlStatement
{
    /// <summary>Represents a SQL JOIN.</summary>
    protected Join(object expression)
    {
        Expression = Guard.NotNull(SQL.Convert(expression), nameof(expression));
    }

    /// <summary>The type (inner, left, right, outer) join.</summary>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public abstract Keyword JoinType { get; }

    /// <summary>The table/select expression to join on.</summary>
    public ISqlStatement Expression { get; }

    /// <summary>The alias to refer to.</summary>
    public Alias Alias { get; internal init; }

    /// <summary>The join condition.</summary>
    public ISqlStatement Condition { get; internal init; } = SyntaxError.JoinOnConditionNotSpecified;

    /// <inherritdoc/>
    public virtual void Write(SqlBuilder builder, int depth)
    {
        Guard.NotNull(builder, nameof(builder));

        builder
            .Indent(depth)
            .Write(JoinType)
            .NewLineOrSpace()
            .Indent(depth + 1)
            .Write(Expression)
        ;

        if (Alias.NotEmpty())
        {
            builder
                .Space().Write(Keyword.AS).Space()
                .Write(Alias)
            ;
        }
        builder
            .Space().Write(Keyword.ON).Space()
            .Write(Condition)
            .NewLineOrSpace();
    }

    /// <inheritdoc/>
    [Pure]
    public sealed override string ToString() => this.UseSqlBuilder();

    /// <summary>Implicitly casts a <see cref="string"/> to a <see cref="Join"/>.</summary>
    public static implicit operator Join(string value) => value is null ? null : new RawJoin(value);

  
}
