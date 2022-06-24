using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using FluentAssertions.Specialized;
using System;

namespace SqlBob.Specs.TestTools
{
    public class SqlStatementAssertions : ReferenceTypeAssertions<SqlStatement, SqlStatementAssertions>
    {
        public SqlStatementAssertions(SqlStatement subject) : base(subject) { }

        protected override string Identifier => "SqlStatement";

        public ExceptionAssertions<SyntaxError> HaveSyntaxError()
        {
            Func<string> minified = () => Subject.Minified();
            return minified.Should().Throw<SyntaxError>();
        }

        public AndConstraint<SqlStatementAssertions> HaveSql(string sql)
        {
            var minified = Subject.Minified();
            Execute.Assertion
                .ForCondition(minified == sql)
                .FailWith($"Expected:\n'{sql}' but found:\n'{minified}'.");
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
