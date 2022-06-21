using SqlBob;

namespace Alias_specs;

public class Created_from
{
    [Test]
    public void string_implictly()
    {
        Alias alias = "c";
        alias.Should().HaveSql("c");
    }

    [Test]
    public void Token_Alias_factory()
        => Token.Alias("c").Should().HaveSql("c");

    [Test]
    public void X()
    {
        Alias alias = "c";
        var literal = alias + "Field";
        literal.Should().HaveSql("c.Field");
    }


    [Test]
    public void Y()
    {
        Alias alias = "c";
        var literal = alias + "Field";
        literal.Should().HaveSql("c.Field");
    }


}

public class Write
{
    [Test]
    public void Guards_SqlBuilder() => Alias.None.Should().GuardSqlBuilder();
}
