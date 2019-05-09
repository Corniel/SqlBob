using SqlBob.Tests.Tools;
using NUnit.Framework;

namespace SqlBob.Tests.Operators
{
    public class NotTest
    {
        [Test]
        public void NOT_NullArgument_IsNull()
        {
            var actual = Logical.Not(null);
            Assert.Null(actual);
        }

        [Test]
        public void NOT_NotArgument_IsParamterArgument()
        {
            Literal expected = "[dbo].[Column] = 17";
            var actual = Logical.Not(Logical.Not(expected));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void NOT_Minified()
        {
            var statement = Logical.Not("[dbo].[Column] = 17");
            SqlAssert.Minified(statement, "NOT([dbo].[Column] = 17)");
        }

        [Test]
        public void NOT_Formatted()
        {
            var statement = Logical.Not("[dbo].[Column] = 17");
            SqlAssert.Formatted(statement,
                "NOT(",
                "    [dbo].[Column] = 17",
                ")",
                "");
        }
    }
}
