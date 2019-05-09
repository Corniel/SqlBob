using SqlBob.Tests.Tools;
using NUnit.Framework;

namespace SqlBob.Tests.Statements
{
    public class FromTest
    {
        private static readonly From TestStatement = new From("Users").As("[u]");

        [Test]
        public void GuardsSqlBuilder() => SqlAssert.GuardsSqlBuilder(TestStatement);

        [Test]
        public void EmptyFrom_HasSyntaxError()=> SqlAssert.HasSyntaxError(new From(null));

        [Test]
        public void Formatted()
        {
            SqlAssert.Formatted(TestStatement,
                "FROM",
                "    Users AS [u]",
                ""
            );
        }

        [Test]
        public void Minified_WithoutAs() => SqlAssert.Minified(new From("Users"), "FROM Users");

        [Test]
        public void Minified() => SqlAssert.Minified(TestStatement, "FROM Users AS [u]");
    }
}
