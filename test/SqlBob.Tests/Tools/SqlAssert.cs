using System;
using System.Diagnostics;
using NUnit.Framework;

namespace SqlBob.Tests.Tools
{
    /// <summary>Specific asserts that should make testing SQL Bob easier.</summary>
    internal static class SqlAssert
    {
        [DebuggerStepThrough]
        public static SqlSyntaxException HasSyntaxError(ISqlStatement statement)
        {
            Assert.NotNull(statement);
            return Assert.Throws<SqlSyntaxException>(() => statement.Minified());
        }

        [DebuggerStepThrough]
        public static void GuardsSqlBuilder(ISqlStatement statement)
        {
            Assert.NotNull(statement);
            Assert.Throws<ArgumentNullException>(() => statement.Write(null));
        }

        [DebuggerStepThrough]
        public static void Minified(ISqlStatement statement, string sql)
        {
            Assert.NotNull(statement);
            Assert.AreEqual(sql, statement.Minified());
        }

        [DebuggerStepThrough]
        public static void Formatted(ISqlStatement statement, params string[] sql)
        {
            Assert.NotNull(statement);
            Assert.AreEqual(string.Join(Environment.NewLine, sql), statement.ToString());
        }
    }
}
