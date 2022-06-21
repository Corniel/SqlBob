namespace SqlBob;

/// <summary>Represents the SELECT clause.</summary>
public class SelectClause : SqlClause<ISqlStatement>
{
    /// <summary>Creates a new instance of a <see cref="SelectClause"/>.</summary>
    public SelectClause(params object[] expressions)
        : base(ConvertAll(expressions)) { }

    /// <inherritdoc/>
    public override void Write(SqlBuilder builder, int depth)
    {
        builder
             .Indent(depth)
             .Write(Keyword.SELECT)
         ;
        if (Count == 0)
        {
            builder.Space().Write(Select.Star, depth).NewLineOrSpace();
        }
        else
        {
            builder
                .Join
                (
                    ",",
                    (qb, select) => qb.NewLineOrSpace().Indent(depth + 1).Write(select),
                    this.ToArray()
                )
                .NewLineOrSpace()
            ;
        }
    }

    /// <summary>Implicitly casts a <see cref="string"/> to a <see cref="SelectClause"/>.</summary>
    public static implicit operator SelectClause(string value) => value is null ? null : new RawSelectClause(value);

    /// <inherritdoc/>
    [Pure]
    protected override ISqlStatement Cast(object arg) => new Select(arg);
}
