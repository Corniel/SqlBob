namespace SqlBob;

/// <summary>Represent a base class for SQL logical statements such as AND, OR, Less Than ect.</summary>
public abstract class Logical : SqlStatement
{
    /// <summary>Creates a new instance of a SQL logical statement.</summary>
    protected Logical(params object[] expressions)
    {
        _epxressions = ConvertAll(expressions);
    }

    /// <summary>The operator keyword.</summary>
    public abstract Keyword Operator { get; }

    /// <summary>Gets the n-th element of the expressions array.</summary>
    protected ISqlStatement this[int index] => _epxressions[index];

    /// <summary>Gets the total of expressions.</summary>
    protected int Count => _epxressions.Length;

    /// <summary>Gets the expressions that together represent the arguments of the logical expression.</summary>
    private readonly ISqlStatement[] _epxressions;

    /// <inheritdoc/>
    public override void Write(SqlBuilder builder, int depth)
    {
        Guard.NotNull(builder, nameof(builder));

        builder
            .Indent(depth)
            .Literal("(")
            .NewLine()
            .Write(this[0], depth + 1)
        ;
        for (var index = 1; index < Count; index++)
        {
            builder
                .Space()
                .Write(Operator)
                .NewLineOrSpace()
                .Write(this[index], depth + 1)
            ;
        }
        builder
            .NewLine()
            .Literal(")")
            .NewLine()
        ;
    }

    /// <summary>Creates a SQL AND statement.</summary>
    /// <returns>
    /// null if no expressions where provided,
    /// a single expression if one expression was provided,
    /// an AND statement in all other cases.
    /// </returns>
    [Pure]
    public static ISqlStatement And(params object[] expressions)
    {
        var statements = ConvertAll(expressions);
        if (statements.Length == 0)
        {
            return null;
        }
        if (statements.Length == 1)
        {
            return statements[0];
        }
        return new And(statements);
    }

    /// <summary>Creates a SQL OR statement.</summary>
    /// <returns>
    /// null if no expressions where provided,
    /// a single expression if one expression was provided,
    /// an OR statement in all other cases.
    /// </returns>
    [Pure]
    public static ISqlStatement Or(params object[] expressions)
    {
        var statements = ConvertAll(expressions);
        if (statements.Length == 0)
        {
            return null;
        }
        if (statements.Length == 1)
        {
            return statements[0];
        }
        return new Or(statements);
    }

    /// <summary>Creates a SQL NOT statement.</summary>
    /// <returns>
    /// null if the expression was null,
    /// the child of the NOT expression when a NOT expression was provided,
    /// a NOT expression in all other cases.
    /// </returns>
    [Pure]
    public static ISqlStatement Not(object expression)
    {
        if (expression is null)
        {
            return null;
        }
        else return expression is Not not
            ? not[0]
            : new Not(expression);
    }

    /// <summary>Creates a SQL statement that is the equivalent of an exclusive or operation.</summary>
    /// <remarks>
    /// As XOR is not supported by SQL (Server), it 
    /// </remarks>
    [Pure]
    public static ISqlStatement Xor(object left, object right)
    {
        var or = Or(left, right);
        var nand = Not(And(left, right));
        return And(or, nand);
    }

    /// <summary>Creates a SQL equal (=) statement.</summary>
    [Pure]
    public static Logical Equal(object left, object right)
        => new LogicalExpression("=", left, right);

    /// <summary>Creates a SQL not equal (&lt;&gt;) statement.</summary>
    [Pure]
    public static Logical NotEqual(object left, object right)
        => new LogicalExpression("<>", left, right);

    /// <summary>Creates a SQL greater than (;&gt;) statement.</summary>
    [Pure]
    public static Logical GreaterThan(object left, object right)
        => new LogicalExpression(">", left, right);

    /// <summary>Creates a SQL greater than (;&gt;=) statement.</summary>
    [Pure]
    public static Logical GreaterThanOrEqual(object left, object right)
        => new LogicalExpression(">=", left, right);

    /// <summary>Creates a SQL less than (;&lt;) statement.</summary>
    [Pure]
    public static Logical LessThan(object left, object right)
        => new LogicalExpression("<", left, right);

    /// <summary>Creates a SQL less than (;&lt;=) statement.</summary>
    [Pure]
    public static Logical LessThanOrEqual(object left, object right)
        => new LogicalExpression("<=", left, right);
}
