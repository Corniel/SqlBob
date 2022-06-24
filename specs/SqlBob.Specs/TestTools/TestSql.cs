namespace SqlBob.Specs.TestTools;

internal static class TestSql
{
    public static Table Table = Schema.Dbo.Table("MyTable").As("t");
    public static Negate Not = (Negate)SQL.Raw("t.Active").Not();
    public static Raw Simple = "SELECT * FROM [Table]";
    public static Selection Selection = SQL.Select("t.Column");
    public static Compare Compare = SQL.Select("t.Column").Eq("42");
}
