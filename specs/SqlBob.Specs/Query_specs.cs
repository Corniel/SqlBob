﻿namespace Query_specs;

public class Can_be_constructed
{
    [Test]
    public void via_SQL_Select()
    {
        var myTable = Schema.Dbo.Table("MyTable").As("[t]");
        var joinTable = Schema.Sys.Table("User").As("u");

        var query = Query
            .Select(myTable.Select(
                "MyCol",
                myTable.Col("myCol").As("alt"),
                "ID"))
            .From(myTable)
            .Join(
                Join.Inner(joinTable).On(joinTable.Col("Id").Eq(myTable.Col("User"))))
            .Where(
                myTable.Col("Date").Lt(SqlFunction.GetUtcDate()))
            .OrderBy(
                myTable.Col("myCol").Desc(),
                Order.By("ID").Asc());

        query.Should().HaveSql(
            "SELECT [t].MyCol, [t].myCol AS alt, [t].ID " +
            "FROM [dbo].MyTable [t] " +
            "INNER JOIN [sys].User u ON u.Id = [t].User " +
            "WHERE [t].Date < GetUtcDate() " +
            "ORDER BY [t].myCol DESC, ID ASC");
    }
}

public class Requires
{
    [Test]
    public void From_clause()
        => Query.Select("*").Should().HasSyntaxError()
        .WithMessage("SQL contains a syntax error: SELECT * FROM /* missing from statement */");
}
