namespace Comparision_specs;

public class Can_be_created_from
{
    static readonly Raw Left = "tb.Date";
    static readonly SqlFunction Right = SqlFunction.GetUtcDate();

    [Test]
    public void Eq() => Left.Eq(Right).Should().HaveSql("tb.Date = GetUtcDate()");

    [Test]
    public void Ne() => Left.Ne(Right).Should().HaveSql("tb.Date <> GetUtcDate()");

    [Test]
    public void Lt() => Left.Lt(Right).Should().HaveSql("tb.Date < GetUtcDate()");
    
    [Test]
    public void Lte() => Left.Lte(Right).Should().HaveSql("tb.Date <= GetUtcDate()");

    [Test]
    public void Gt() => Left.Gt(Right).Should().HaveSql("tb.Date > GetUtcDate()");

    [Test]
    public void Gte() => Left.Gte(Right).Should().HaveSql("tb.Date >= GetUtcDate()");
}
public class Write
{
    [Test]
    public void Guards_SqlBuilder() => TestSql.Compare.Should().GuardSqlBuilder();
}
