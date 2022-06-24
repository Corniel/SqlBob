namespace Missing_specs;

public class Has
{
    [Test]
    public void SQL_output_indicating_syntax_error() => TestSql.Missing.Should().HaveSyntaxError()
        .And.Message.Should().Be("SQL contains a syntax error: /* missing from */");
}
public class Write
{
    [Test]
    public void Guards_SqlBuilder() => TestSql.Missing.Should().GuardSqlBuilder();
}
