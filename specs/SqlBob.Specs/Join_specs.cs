namespace Join_specs;

public class Can_be_created_as
{
    [Test]
    public void Left_Join_on_condition()
        => Join.Left(TestSql.Table).On("t.ID = o.Table").Should().HaveSql("LEFT JOIN [dbo].MyTable t ON t.ID = o.Table");

    [Test]
    public void Right_Join_on_condition()
       => Join.Right(TestSql.Table).On("t.ID = o.Table").Should().HaveSql("RIGHT JOIN [dbo].MyTable t ON t.ID = o.Table");

    [Test]
    public void Inner_Join_on_condition()
       => Join.Inner(TestSql.Table).On("t.ID = o.Table").Should().HaveSql("INNER JOIN [dbo].MyTable t ON t.ID = o.Table");
    [Test]
    public void Full_Outher_Join_on_condition()
       => Join.FullOuther(TestSql.Table).On("t.ID = o.Table").Should().HaveSql("FULL OUTHER JOIN [dbo].MyTable t ON t.ID = o.Table");
}

public class Requires_both_table_and_condition_for
{
    [Test]
    public void Left_Join()
       => Join.Left(null).Should().HaveSyntaxError()
        .And.Message.Should().Be("SQL contains a syntax error: LEFT JOIN /* missing table */ ON /* missing condition */");

    [Test]
    public void Right_Join()
       => Join.Right(null).Should().HaveSyntaxError()
        .And.Message.Should().Be("SQL contains a syntax error: RIGHT JOIN /* missing table */ ON /* missing condition */");

    [Test]
    public void Inner_Join()
       => Join.Inner(null).Should().HaveSyntaxError()
        .And.Message.Should().Be("SQL contains a syntax error: INNER JOIN /* missing table */ ON /* missing condition */");

    [Test]
    public void Full_Outher_Join()
        => Join.FullOuther(null).Should().HaveSyntaxError()
        .And.Message.Should().Be("SQL contains a syntax error: FULL OUTHER JOIN /* missing table */ ON /* missing condition */");
}

public class Write
{
    [Test]
    public void Guards_SqlBuilder() => Join.Inner("table t").On("t.ID = o.Table").Should().GuardSqlBuilder();
}
