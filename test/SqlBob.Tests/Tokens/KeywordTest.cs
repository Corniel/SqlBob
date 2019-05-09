using SqlBob.Tests.Tools;
using NUnit.Framework;

namespace SqlBob.Tests.Tokens
{
    public class KeywordTest
    {
        [Test]
        public void GuardsSqlBuilder() => SqlAssert.GuardsSqlBuilder(Keyword.AS);

        [Test]
        public void Equals_number_False() => Assert.False(Keyword.ASC.Equals(13));

        [Test]
        public void Equals_DifferentKeyword_False() => Assert.False(Keyword.DESC.Equals(Keyword.AS));

        [Test]
        public void Equals_SameKeyword_True() => Assert.True(Keyword.DESC.Equals(Keyword.DESC));

        [Test]
        public void DebuggerDisplay() => Assert.AreEqual("{Keyword} ON", Keyword.ON.DebuggerDisplay);
    }
}
