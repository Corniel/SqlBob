using SqlBob.Tests.Tools;
using NUnit.Framework;

namespace SqlBob.Tests.Queries
{
    public class SelectQueryTest
    {
        [Test]
        public void From_NotSpecified_HasSyntaxError()
        {
            var query = Query.Select("*");

            Assert.IsInstanceOf<From>(query.FromClause);
            SqlAssert.HasSyntaxError(query);
        }

        [Test]
        public void From_Expression()
        {
            var query = Query.Select("*").From("Users", "[u]");

            Assert.IsInstanceOf<From>(query.FromClause);
            SqlAssert.Minified(query.FromClause, "FROM Users AS [u]");
        }

        [Test]
        public void From_FromStatement()
        {
            var query = Query.Select("*").From(new From("Users").As("[u]"));

            Assert.IsInstanceOf<From>(query.FromClause);
            SqlAssert.Minified(query.FromClause, "FROM Users AS [u]");
        }

        [Test]
        public void Where_NotSpecified()
        {
            var query = Query.Select("*");
            Assert.IsInstanceOf<Where>(query.WhereClause);
            Assert.AreEqual(Where.None, query.WhereClause);
       }

        [Test]
        public void Where_Expession()
        {
            var query = Query.Select("*").Where("u.Name LIKE '%Bob%'");
            Assert.IsInstanceOf<Where>(query.WhereClause);
            SqlAssert.Minified(query.WhereClause, "WHERE u.Name LIKE '%Bob%'");
        }

        [Test]
        public void Where_WhereStatement()
        {
            var query = Query.Select("*").Where(new Where("u.Name LIKE '%Bob%'"));
            Assert.IsInstanceOf<Where>(query.WhereClause);
            SqlAssert.Minified(query.WhereClause, "WHERE u.Name LIKE '%Bob%'");
        }
    }
}
