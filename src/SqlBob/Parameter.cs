namespace SqlBob;

public sealed class Parameter : SqlStatement
{
    internal Parameter(string? name) => Name = Guard.NotNullOrEmpty(name?.TrimStart('@'), nameof(name));

    /// <summary>Gets the name of the SQL parameter.</summary>
    public string Name { get; }

    /// <inheritdoc />
    public override void Write(SqlBuilder builder, int depth)
        => Guard.NotNull(builder, nameof(builder)).Indent(depth).Literal('@').Literal(Name);

    /// <summary>Implicitly casts a <see cref="string"/> to a SQL <see cref="Parameter"/>.</summary>
    public static implicit operator Parameter(string? value) => new(value);
}
