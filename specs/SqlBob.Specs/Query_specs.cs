namespace Query_specs;

public class Can_be_constructed
{
    [Test]
    public void via_SQL_Select()
    {
        var myTable = Schema.Dbo.Table("MyTable").As("[t]");

        var query = Query
            .Select(myTable.Select(
                "MyCol",
                myTable.Col("myCol").As("alt"),
                "ID"))
            .From(myTable)
            .Where(myTable.Col("Date").Lt(SqlFunction.GetUtcDate()));

        query.Should().HaveSql("SELECT [t].MyCol, [t].myCol AS alt, [t].ID FROM [dbo].MyTable [t] WHERE [t].Date < GetUtcDate()");
    }
}
