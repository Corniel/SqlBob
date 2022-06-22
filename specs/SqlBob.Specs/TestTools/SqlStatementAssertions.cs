using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using System;

namespace SqlBob.Specs.TestTools
{
    public class SqlStatementAssertions : ReferenceTypeAssertions<SqlStatement, SqlStatementAssertions>
    {
        public SqlStatementAssertions(SqlStatement subject) : base(subject) { }

        protected override string Identifier => "SqlStatement";

        public AndConstraint<SqlStatementAssertions> HasSyntaxError()
        {
            Func<string> minified = () => Subject.Minified();
            minified.Should().Throw<Exception>();
            return new(this);
        }

        public AndConstraint<SqlStatementAssertions> HaveSql(string sql)
        {
            var minified = Subject.Minified();
            Execute.Assertion
                .ForCondition(minified == sql)
                .FailWith($"Expected '{sql}' but fount '{minified}'.");
            return new(this);
        }

        public AndConstraint<SqlStatementAssertions> GuardSqlBuilder()
        {
            Action write = () => Subject.Write(null);
            write.Should().Throw<ArgumentNullException>();
            return new(this);
        }
    }
}
