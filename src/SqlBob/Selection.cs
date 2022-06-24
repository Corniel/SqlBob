namespace SqlBob;

public sealed class Selection : SqlStatement
{
    internal Selection(SqlStatement expression, Alias alias)
    {
        Expression = expression;
        Alias = alias;
    }

    public SqlStatement Expression { get; }
    
    public Alias Alias { get; }

    [Pure]
    public override Selection As(Alias alias) => new(Expression, alias);

    /// <inheritdoc />
    public override void Write(SqlBuilder builder, int depth)
    {
        Guard.NotNull(builder, nameof(builder)).Write(Expression, depth);
        if (Alias.NotEmpty())
        {
            builder.Space().Write(Keyword.AS).Space().Write(Alias);
        }
    }
}
