namespace Keyword_specs;

public class Write
{
    [Test]
    public void Guards_SqlBuilder() => Keyword.AND.Should().GuardSqlBuilder();
}
