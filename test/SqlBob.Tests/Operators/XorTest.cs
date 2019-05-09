using SqlBob.Tests.Tools;
using NUnit.Framework;

namespace SqlBob.Tests.Operators
{
    public class XorTest
    {
        [Test]
        public void XOR_TwoArguments_Simplified()
        {
            var statement = Logical.Xor("@A", "@B");
            SqlAssert.Minified(statement, "((@A OR @B) AND NOT((@A AND @B)))");
        }
    }
}
