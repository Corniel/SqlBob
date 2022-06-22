namespace SqlBob
{
    public sealed record Where : SqlStatement
    {
        public static readonly Where None = new();

        public override void Write(SqlBuilder builder, int depth)
        {
            throw new NotImplementedException();
        }
    }
}
