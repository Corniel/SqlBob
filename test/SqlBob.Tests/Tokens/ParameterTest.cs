using SqlBob.Tests.Tools;
using NUnit.Framework;

namespace SqlBob.Tests.Tokens
{
    public class ParameterTest
    {
        private static readonly Parameter TestStatement = "name";

        [Test]
        public void GuardsSqlBuilder() => SqlAssert.GuardsSqlBuilder(TestStatement);

        [TestCase("", "")]
        [TestCase("name", "@name")]
        [TestCase("@name", "@name")]
        public void Ctor(string param, string expected)
        {
            Parameter par = param;
            Assert.AreEqual(expected, par.ToString());
        }

        [Test]
        public void Equals_number_False() => Assert.False(TestStatement.Equals(13));

        [Test]
        public void Equals_DifferentKeyword_False() => Assert.False(TestStatement.Equals(new Parameter("id")));

        [Test]
        public void Equals_SameKeyword_True() => Assert.True(TestStatement.Equals(new Parameter("@Name")));

        [Test]
        public void DebuggerDisplay() => Assert.AreEqual("{Parameter} @name", TestStatement.DebuggerDisplay);
    }
}
