using SqlBob.Formatting;
using NUnit.Framework;

namespace SqlBob.Tests.Formatting
{
    public class SqlFormatInfoTest
    {
        [Test]
        public void UseLowerCase() => Assert.AreEqual("select", Keyword.SELECT.ToString(new SqlFormatInfo { UseUpperCase = false }));
    }
}
