using SqlBob.Tests.Tools;
using NUnit.Framework;

namespace SqlBob.Tests.Statements
{
    public class WhereTest
    {
        private static readonly Where TestStatement = new Where("[u].Id = @id");

        [Test]
        public void GuardsSqlBuilder() => SqlAssert.GuardsSqlBuilder(TestStatement);

        [Test]
        public void Minified_None() => SqlAssert.Minified(Where.None, "");


        [Test]
        public void Minified() => SqlAssert.Minified(TestStatement, "WHERE [u].Id = @id");

        [Test]
        public void Formatted()
        {
            SqlAssert.Formatted(TestStatement,
                "WHERE",
                "    [u].Id = @id",
                "");
        }

        [Test]
        public void DebuggerDisplay_None() => Assert.AreEqual("{Where} /* No WHERE statement */", Where.None.DebuggerDisplay);
    }
}
