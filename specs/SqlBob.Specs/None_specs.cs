namespace None_specs;

public class Has
{
    [Test]
    public void no_SQL_output() => TestSql.None.Should().HaveSql(string.Empty);
}
public class Write
{
    [Test]
    public void Guards_SqlBuilder() => TestSql.None.Should().GuardSqlBuilder();
}
