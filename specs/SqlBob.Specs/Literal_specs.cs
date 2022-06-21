namespace Literal_specs;

public class Write
{
    [Test]
    public void Guards_SqlBuilder() => Literal.None.Should().GuardSqlBuilder();
}
