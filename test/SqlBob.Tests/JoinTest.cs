using SqlBob.Tests.Tools;
using NUnit.Framework;

namespace SqlBob.Tests
{
    public class JoinTest
    {
        [Test]
        public void OnIsNull_SyntaxError()
        {
            var join = Join.Inner("Persons").As("p").On(null);
            SqlAssert.HasSyntaxError(join);
        }

        [Test]
        public void OnIsNull_ToString_WithSyntaxErrorMessage()
        {
            var join = Join.Inner("Persons").As("p").On(null);
            Assert.AreEqual(
@"INNER JOIN
    Persons AS p ON /* Can't apply a JOIN without an ON condition. */

", join.ToString());
        }

        [Test]
        public void GuardsSqlBuilder() => SqlAssert.GuardsSqlBuilder(Join.Left("Persons"));

        [Test]
        public void OnConditionMissing_HasSyntaxError()=> SqlAssert.HasSyntaxError(Join.Left("Persons"));

        [Test]
        public void Formatted()
        {
            var join = Join.Inner("Persons").As("p").On("p.ID = u.Person");
            SqlAssert.Formatted(join,
                "INNER JOIN",
                "    Persons AS p ON p.ID = u.Person",
                "");
        }

        [Test]
        public void InnerJoin_WithCondition()
        {
            var join = Join.Inner("Persons").As("p").On("p.ID = u.Person");
            Assert.IsInstanceOf<InnerJoin>(join);
            SqlAssert.Minified(join,
                "INNER JOIN Persons AS p ON p.ID = u.Person");
        }

        [Test]
        public void LeftJoin_WithCondition()
        {
            var join = Join.Left("Persons").As("p").On("p.ID = u.Person");
            Assert.IsInstanceOf<LeftJoin>(join);
            SqlAssert.Minified(join, 
                "LEFT JOIN Persons AS p ON p.ID = u.Person");
        }

        [Test]
        public void RightJoin_WithCondition()
        {
            var join = Join.Right("Persons").As("p").On("p.ID = u.Person");

            Assert.IsInstanceOf<RightJoin>(join);
            SqlAssert.Minified(join, "RIGHT JOIN Persons AS p ON p.ID = u.Person");
        }

        [Test]
        public void FullOutherJoin_WithCondition()
        {
            var join = Join.FullOuther("Persons").As("p").On("p.ID = u.Person");

            Assert.IsInstanceOf<FullOutherJoin>(join);
            SqlAssert.Minified(join, "FULL OUTHER JOIN Persons AS p ON p.ID = u.Person");
        }

        [Test]
        public void RawJoin_SomeRawSqlString()
        {
            var join = Join.Raw("INNER JOIN Users AS u ON u.Id = p.User");

            Assert.IsInstanceOf<RawJoin>(join);
            SqlAssert.Minified(join, "INNER JOIN Users AS u ON u.Id = p.User");
            Assert.AreEqual(default(Keyword), join.JoinType);
        }

         [Test]
        public void RawJoin_Null_IsNull()
        {
            var join = Join.Raw(null);
            Assert.Null(join);
        }
  
        [Test]
        public void Implicit_FromString()
        {
            Join join = "INNER JOIN Users AS u ON u.Id = p.User";

            Assert.IsInstanceOf<RawJoin>(join);
            SqlAssert.Minified(join, "INNER JOIN Users AS u ON u.Id = p.User");
            Assert.AreEqual(default(Keyword), join.JoinType);
        }
    }
}
