namespace SqlBob;

public sealed record Selection : SqlStatement
{
    public static readonly Selection Star = new(SQL.Raw("*"), Alias.None);

    internal Selection(SqlStatement expression, Alias alias)
    {
        Expression = expression;
        Alias = alias;
    }

    public SqlStatement Expression { get; }
    
    public Alias Alias { get; }

    public Selection As(Alias alias) => new(Expression, alias);

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
