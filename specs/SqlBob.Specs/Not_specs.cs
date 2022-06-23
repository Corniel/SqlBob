namespace Not_specs;

public class Can_be_created_from
{
    [Test]
    public void Negating_an_expression() 
        => SQL.Raw("t.Active").Not().Should().BeOfType<Negate>().And.HaveSql("NOT(t.Active)");
}

public class Can_be_reverted
{
    [Test]
    public void By_double_negation()
        => SQL.Raw("t.Active").Not().Not().Should().NotBeOfType<Negate>().And.HaveSql("t.Active");
}

public class Write
{
    [Test]
    public void Guards_SqlBuilder() => TestSql.Not.Should().GuardSqlBuilder();
}
