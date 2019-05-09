using NUnit.Framework;

namespace SqlBob.Tests
{
    public class SqlStatementConvertTest
    {
        [Test]
        public void Convert_Null_Null() => Assert.Null(SqlStatement.Convert(null));

        [Test]
        public void Convert_StringEmpty_Null() => Assert.Null(SqlStatement.Convert(string.Empty));

        [Test]
        public void Convert_Int_Literal()
        {
            var statement = SqlStatement.Convert(17);
            Assert.IsInstanceOf<Literal>(statement);
            Assert.AreEqual("17", statement.ToString());
        }

        [Test]
        public void Convert_Statement_Guards()
        {
            object expression = Keyword.DESC;
            var statement = SqlStatement.Convert(expression);
            Assert.IsInstanceOf<Keyword>(statement);
            Assert.AreEqual(expression, statement);
        }

        [Test]
        public void ConvertAll_Null_EmptyArray() => CollectionAssert.IsEmpty(SqlStatement.ConvertAll((object[])null));

        [Test]
        public void ConvertAll_NullArgs_AllSkipped() => CollectionAssert.IsEmpty(SqlStatement.ConvertAll(null, ""));

        [Test]
        public void ConvertAll_SomeElements()
        {
            var converted = SqlStatement.ConvertAll("SELECT *", Keyword.FROM);

            var expected = new ISqlStatement[]
            {
                (Literal)"SELECT *",
                Keyword.FROM,
            };

            Assert.AreEqual(expected, converted);
        }

        [Test]
        public void Trim_Null_Empty() => CollectionAssert.IsEmpty(SqlStatement.Trim<Literal>(null));

        [Test]
        public void Trim_NullAndNotNull_Single() => Assert.AreEqual(1, SqlStatement.Trim(new[] { default(Literal), "TEST" }).Length);
    }
}
