namespace Raw_specs;

public class Created_from
{
    [Test]
    public void string_via_implicit_cast()
    {
        Raw sql = "SELECT * FROM [MyTable]";
        sql.Should().HaveSql("SELECT * FROM [MyTable]");
    }

    [Test]
    public void SQL_Raw_method()
    {
        var sql = SQL.Raw("SELECT * FROM [MyTable]");
        
        sql.Should()
            .BeOfType<Raw>()
            .And.HaveSql("SELECT * FROM [MyTable]");
    }
}

public class Write
{
    [Test]
    public void Guards_SqlBuilder() => SQL.Raw("SELECT *").Should().GuardSqlBuilder();
}
