namespace SqlBob
{
    public partial class SqlFunction
    {
        /// <summary>Returns a Count(*) SQL function.</summary>
        public static SqlFunction Count() => new SqlFunction(nameof(Count), "*");

        /// <summary>Returns a GetDate() SQL function.</summary>
        public static SqlFunction GetDate() => new SqlFunction(nameof(GetDate));

        /// <summary>Returns a GetUtcDate() SQL function.</summary>
        public static SqlFunction GetUtcDate() => new SqlFunction(nameof(GetUtcDate));
    }
}
