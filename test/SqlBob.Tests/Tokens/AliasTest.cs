using SqlBob.Tests.Tools;
using NUnit.Framework;

namespace SqlBob.Tests.Tokens
{
    public class AliasTest
    {
        private static readonly Alias TestStatement = "[u]";

        [Test]
        public void GuardsSqlBuilder() => SqlAssert.GuardsSqlBuilder(TestStatement);

        [Test]
        public void Add_Literal()
        {
            Alias alias = "tab";
            Literal literal = "[Identifier]";
            var statement = alias + literal;

            SqlAssert.Minified(statement, "tab.[Identifier]");
        }

        [Test]
        public void Add_LiteralToNone()
        {
            var alias = Alias.None;
            var statement = alias + "[Identifier]";

            SqlAssert.Minified(statement, "[Identifier]");
        }

        [Test]
        public void Add_String()
        {
            Alias alias = "tab";
            var statement = alias + "[Identifier]";

            SqlAssert.Minified(statement, "tab.[Identifier]");
        }

        [Test]
        public void ToString_ShouldEqual()
        {
            Alias alias = "tab";
            Assert.AreEqual("tab", alias.ToString());
        }

        [Test]
        public void DebuggerDisplay() => Assert.AreEqual("{Alias} [u]", TestStatement.DebuggerDisplay);
    }
}
