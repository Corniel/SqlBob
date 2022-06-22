namespace Query_specs;

public class Can_be_constructed
{
    [Test]
    public void via_SQL_Select()
    {
        var query = Query
            .Select("*")
            .From("MyTable [t]")
            .Where("[t].Date < GetUtcDate()");

        query.Should().HaveSql("SELECT * FROM MyTable [t] WHERE [t].Date < GetUtcDate()");
    }
}
