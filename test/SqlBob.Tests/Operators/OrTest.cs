using SqlBob.Tests.Tools;
using NUnit.Framework;

namespace SqlBob.Tests.Operators
{
    public class OrTest
    {
        [Test]
        public void Or_NullArgument_IsNull()
        {
            object[] args = null;
            var actual = Logical.Or(args);
            Assert.Null(actual);
        }

        [Test]
        public void Or_NoArgument_IsNull()
        {
            var actual = Logical.Or();
            Assert.Null(actual);
        }

        [Test]
        public void Or_1Argument_IsParamterArgument()
        {
            Literal expected = "[dbo].[Column] = 17";
            var actual = Logical.Or(expected);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Or_2Argument_Minified()
        {
            var statement = Logical.Or("[dbo].[Column] = 17", "[dbo].[Id] <> 666");
            SqlAssert.Minified(statement, "([dbo].[Column] = 17 OR [dbo].[Id] <> 666)");
        }

        [Test]
        public void Or_2Argument_Formatted()
        {
            var statement = Logical.Or("[dbo].[Column] = 17", "[dbo].[Id] <> 666");
            SqlAssert.Formatted(statement,
                "(",
                "    [dbo].[Column] = 17 OR",
                "    [dbo].[Id] <> 666",
                ")",
                "");
        }
    }
}
