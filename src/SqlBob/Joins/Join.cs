namespace SqlBob;

/// <summary>Represents a SQL JOIN.</summary>
public abstract class Join : SqlStatement
{
    /// <summary>Represents a SQL JOIN.</summary>
    protected Join(object expression) => Expression = Guard.NotNull(Convert(expression), nameof(expression));

    /// <summary>The type (inner, left, right, outer) join.</summary>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public abstract Keyword JoinType { get; }

    /// <summary>The table/select expression to join on.</summary>
    public ISqlStatement Expression { get; }

    /// <summary>The alias to refer to.</summary>
    public Alias Alias { get; internal protected set; }

    /// <summary>The join condition.</summary>
    public ISqlStatement Condition { get; internal protected set; } = SyntaxError.JoinOnConditionNotSpecified;

    /// <inherritdoc/>
    public override void Write(SqlBuilder builder, int depth = 0)
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

    /// <summary>Implicitly casts a <see cref="string"/> to a <see cref="Join"/>.</summary>
    public static implicit operator Join(string value) => value is null ? null : new RawJoin(value);

    /// <summary>Creates a JOIN based on a raw SQL <see cref="string"/>.</summary>
    [Pure]
    public static Join Raw(string value) => value;

    /// <summary>Creates an INNER JOIN.</summary>
    [Pure]
    public static InnerJoin Inner(object expression) => new(expression);

    /// <summary>Creates an LEFT (OUTER) JOIN.</summary>
    [Pure]
    public static LeftJoin Left(object expression) => new(expression);

    /// <summary>Creates an RIGHT (OUTER) JOIN.</summary>
    [Pure]
    public static RightJoin Right(object expression) => new(expression);

    /// <summary>Creates an FULL OUTER JOIN.</summary>
    [Pure]
    public static FullOutherJoin FullOuther(object expression) => new(expression);
}
