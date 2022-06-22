namespace SqlBob.Specs;

public class Can_be_created
{
    [Test]
    public void SQL_Select_method()
    {
        var sql = SQL.Select("t.Column");

        sql.Should()
            .BeOfType<Selection>()
            .And.HaveSql("t.Column");
    }

    [Test]
    public void SQL_Select_method_with_alias()
    {
        var sql = SQL.Select("t.Column").As("OtherColumn");

        sql.Should()
            .BeOfType<Selection>()
            .And.HaveSql("t.Column AS OtherColumn");
    }
}

public class Write
{
    [Test]
    public void Guards_SqlBuilder() => TestSql.Selection.Should().GuardSqlBuilder();
}
