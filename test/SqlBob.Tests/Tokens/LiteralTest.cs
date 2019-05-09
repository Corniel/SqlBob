using SqlBob.Tests.Tools;
using NUnit.Framework;

namespace SqlBob.Tests.Tokens
{
    public class LiteralTest
    {
        private static readonly Literal TestStatement = "user.Id = @userId";

        [Test]
        public void GuardsSqlBuilder() => SqlAssert.GuardsSqlBuilder(TestStatement);

        [Test]
        public void DebuggerDisplay() => Assert.AreEqual("{Literal} user.Id = @userId", TestStatement.DebuggerDisplay);
    }
}
