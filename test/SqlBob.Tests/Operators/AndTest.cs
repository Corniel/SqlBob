using SqlBob.Tests.Tools;
using NUnit.Framework;

namespace SqlBob.Tests.Operators
{
    public class AndTest
    {
        [Test]
        public void And_NullArgument_IsNull()
        {
            object[] args = null;
            var actual = Logical.And(args);
            Assert.Null(actual);
        }

        [Test]
        public void And_NoArgument_IsNull()
        {
            var actual = Logical.And();
            Assert.Null(actual);
        }

        [Test]
        public void And_1Argument_IsParamterArgument()
        {
            Literal expected = "[dbo].[Column] = 17";
            var actual = Logical.And(expected);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void And_2Argument_Minified()
        {
            var statement = Logical.And("[dbo].[Column] = 17", "[dbo].[Id] <> 666");
            SqlAssert.Minified(statement, "([dbo].[Column] = 17 AND [dbo].[Id] <> 666)");
        }

        [Test]
        public void And_2Argument_Formatted()
        {
            var statement = Logical.And("[dbo].[Column] = 17", "[dbo].[Id] <> 666");
            SqlAssert.Formatted(statement,
                "(",
                "    [dbo].[Column] = 17 AND",
                "    [dbo].[Id] <> 666",
                ")",
                "");
        }
    }
}
