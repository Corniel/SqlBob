namespace SqlBob.Specs.TestTools;

internal static class TestSql
{
    public static Raw Simple = "SELECT * FROM [Table]";

    public static Selection Selection = SQL.Select("t.Column");
}
