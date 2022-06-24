namespace SqlBob;

[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(Diagnostics.CollectionDebugView))]
public abstract class SqlStatements : IReadOnlyCollection<SqlStatement>
{
    public static readonly SqlStatements None = new Empty();

    private SqlStatements() { }

    public abstract int Count { get; }

    [Pure]
    public SqlStatements Add(SqlStatement? statement)
        => statement is not null
        ? new Single(this, statement)
        : this;

    [Pure]
    public SqlStatements AddRange(IEnumerable<SqlStatement> statements)
      => statements is not null
      ? new Multiple(this, statements.ToArray())
      : this;

    [Pure]
    public abstract IEnumerator<SqlStatement> GetEnumerator();

    [Pure]
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private sealed class Empty : SqlStatements
    {
        public override int Count => 0;

        [Pure]
        public override IEnumerator<SqlStatement> GetEnumerator() => Enumerable.Empty<SqlStatement>().GetEnumerator();
    }
    private sealed class Single : SqlStatements
    {
        public Single(SqlStatements parent, SqlStatement current)
        {
            Parent = parent;
            Current = current;
        }

        private readonly SqlStatements Parent;
        private readonly SqlStatement Current;

        public override int Count => Parent.Count + 1;
        
        [Pure]
        public override IEnumerator<SqlStatement> GetEnumerator()
            => Parent.Append(Current).GetEnumerator();
    }

    private sealed class Multiple : SqlStatements
    {
        public Multiple(SqlStatements parent, SqlStatement[] current)
        {
            Parent = parent;
            Current = current;
        }

        private readonly SqlStatements Parent;
        private readonly SqlStatement[] Current;

        public override int Count => Parent.Count + Current.Length;

        [Pure]
        public override IEnumerator<SqlStatement> GetEnumerator()
            => Parent.Concat(Current).GetEnumerator();
    }
}
