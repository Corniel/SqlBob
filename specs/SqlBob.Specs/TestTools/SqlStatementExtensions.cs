using SqlBob.Specs.TestTools;

namespace FluentAssertions;

public static class SqlStatementExtensions
{
    public static SqlStatementAssertions Should<TSqlStatement>(this TSqlStatement statement)
        where TSqlStatement : SqlStatement
        => new(statement);
}
