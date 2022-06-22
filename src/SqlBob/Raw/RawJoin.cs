namespace SqlBob;

/// <summary>To support custom <see cref="Join"/>s based on a <see cref="string"/>.</summary>
internal sealed record RawJoin : Join
{
    /// <summary>Creates a new instance of a raw <see cref="Join"/>.</summary>
    internal RawJoin(Literal raw) : base(raw) { }

    /// <inherritdoc/>
    public override Keyword JoinType => default;

    /// <inherritdoc/>
    public override void Write(SqlBuilder builder, int depth)
    {
        Guard.NotNull(builder, nameof(builder));
        builder
            .Indent(depth)
            .Write(Expression, depth)
            .NewLineOrSpace();
    }
}
