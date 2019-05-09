using SqlBob.Tests.Tools;

namespace SqlBob.Tests.Operators
{
    public class CompareTest
    {
        internal static readonly Literal left = "[Age]";
        internal static readonly Literal right = "@age";

        public void Equal() => SqlAssert.Minified(Logical.Equal(left, right), "([Age] = @age)");
        public void NotEqual() => SqlAssert.Minified(Logical.NotEqual(left, right), "([Age] <> @age)");
        public void LessThan() => SqlAssert.Minified(Logical.LessThan(left, right), "([Age] < @age)");
        public void LessThanOrEqual() => SqlAssert.Minified(Logical.LessThanOrEqual(left, right), "([Age] <= @age)");
        public void GreaterThan() => SqlAssert.Minified(Logical.GreaterThan(left, right), "([Age] > @age)");
        public void GreaterThanOrEqual() => SqlAssert.Minified(Logical.GreaterThanOrEqual(left, right), "([Age] >= @age)");
    }
}
