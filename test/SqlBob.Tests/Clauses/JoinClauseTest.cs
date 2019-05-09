using SqlBob.Tests.Tools;
using NUnit.Framework;

namespace SqlBob.Tests.Clauses
{
    public class JoinClauseTest
    {
        [Test]
        public void Ctor_SupportsDifferentTypes()
        {
            var clause = new JoinClause(
                "INNER JOIN Users AS u ON u.Person = p.ID",
                Join.Left("Address").As("a").On("a.Id = p.Address"));

            SqlAssert.Formatted(clause,
                "INNER JOIN Users AS u ON u.Person = p.ID",
                "LEFT JOIN",
                "    Address AS a ON a.Id = p.Address",
                "");
        }
    }
}
