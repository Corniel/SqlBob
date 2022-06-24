namespace Query_specs;

public class Can_be_constructed
{
    [Test]
    public void via_SQL_Select()
    {
        var myTable = Schema.Dbo.Table("MyTable").As("[t]");
        var joinTable = Schema.Sys.Table("User").As("u");

        var query = SQL.Query
            .Select(myTable.Select(
                "MyCol",
                myTable.Column("myCol").As("alt"),
                "ID"))
            .From(myTable)
            .Join(
                Join.Inner(joinTable).On(joinTable.Column("Id").Eq(myTable.Column("User"))))
            .Where(
                myTable.Column("Date").Lt(SqlFunction.GetUtcDate()))
            .OrderBy(
                myTable.Column("myCol").Desc(),
                Order.By("ID").Asc())
            .Offset(15).Fetch(5);

        query.Should().HaveSql( ""
            + "SELECT [t].MyCol, [t].myCol AS alt, [t].ID "
            + "FROM [dbo].MyTable [t] "
            + "WHERE [t].Date < GetUtcDate() "
            + "INNER JOIN [sys].User u ON u.Id = [t].User "
            + "ORDER BY [t].myCol DESC, ID ASC "
            + "OFFSET 15 ROWS FETCH NEXT 5 ROWS ONLY");
    }
}

public class Requires
{
    [Test]
    public void Both_Select_and_From_clause()
        => SQL.Query.Should().HaveSyntaxError()
        .WithMessage("SQL contains a syntax error: /* missing select */ /* missing from */");
}
