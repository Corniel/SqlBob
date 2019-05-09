using NUnit.Framework;
using SqlBob.Tests.Tools;

namespace SqlBob.Tests.Statements
{
    public class OrderByTest
    {
        [Test]
        public void Parse_WithDESC() => SqlAssert.Minified(OrderBy.Parse("[Name] deSC"), "[Name] DESC");

        [Test]
        public void Parse_WithASC() => SqlAssert.Minified(OrderBy.Parse("[Name] asc"), "[Name] ASC");

        [Test]
        public void Parse_WithoutOrderBy() => SqlAssert.Minified(OrderBy.Parse("[Name]"), "[Name] ASC");
    }
}
