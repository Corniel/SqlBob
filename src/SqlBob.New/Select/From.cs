namespace SqlBob
{
    public sealed record From : SqlStatement
    {
        public static readonly From Star = new();

        public override void Write(SqlBuilder builder, int depth)
        {
            throw new NotImplementedException();
        }
    }
}
