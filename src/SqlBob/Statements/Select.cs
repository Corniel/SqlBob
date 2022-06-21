namespace SqlBob;

/// <summary>Represents a SQL SELECT statement.</summary>
public class Select : SqlStatement
{
    /// <summary>The SELECT * statement.</summary>
    public static readonly Literal Star = "*";

    /// <summary>Creates a new instance of a <see cref="Select"/> statement.</summary>
    public Select(object expression) => Expression = Convert(expression);

    /// <summary>Gets the expression to select.</summary>
    public ISqlStatement Expression { get; }

    /// <summary>Gets the Alias (if any).</summary>
    public Alias Alias { get; internal set; }

    /// <summary>Defines an <see cref="SqlBob.Alias"/> for the SELECT statement.</summary>
    [Impure]
    public Select As(Alias alias)
    {
        Alias = alias;
        return this;
    }

    /// <inheritdoc />
    public override void Write(SqlBuilder builder, int depth = 0)
    {
        builder.Write(Expression);
        if (Alias.NotEmpty())
        {
            builder.Space().Write(Keyword.AS).Space().Write(Alias);
        }
    }
}
