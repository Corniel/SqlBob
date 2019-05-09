using SqlBob.Tests.Tools;
using NUnit.Framework;

namespace SqlBob.Tests.Statements
{
    public class ParenthesisTest
    {
        [Test]
        public void Write_NotSet()
        {
            SqlStatement statement = null;

            var error = SqlAssert.HasSyntaxError(statement.Parenthesize());

            Assert.AreEqual("(/* Can't parenthesize an empty SQL statement. */)", error.Message);
        }
    }
}
