namespace Parenthesis_specs;

public class Adds_curly_barceses
{
    [Test]
    public void arround_SQL_statement()
        => TestSql.Simple.Parenthesis().Should().HaveSql("(SELECT * FROM [Table])");

    [Test]
    public void but_not_arround_parenthesed_statements()
        => TestSql.Simple.Parenthesis().Parenthesis().Should().HaveSql("(SELECT * FROM [Table])");
}
