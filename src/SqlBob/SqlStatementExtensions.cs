using SqlBob.Formatting;
using System;

namespace SqlBob
{
    /// <summary>Generic extensions on <see cref="ISqlStatement"/> that
    /// should be the same for all implementations.
    /// </summary>
    public static class SqlStatementExtensions
    {
        /// <summary>Gets a formatted representation of th SQL statement.</summary>
        /// <param name="sqlStatement">
        /// The SQL statement to represent as <see cref="string"/>.
        /// </param>
        [Obsolete("Use ToString() instead")]
        public static string Formatted(this ISqlStatement sqlStatement) => sqlStatement.ToString(SqlFormatInfo.Debugger);

        /// <summary>Gets a minified representation of th SQL statement.</summary>
        /// <param name="sqlStatement">
        /// The SQL statement to represent as <see cref="string"/>.
        /// </param>
        public static string Minified(this ISqlStatement sqlStatement) => sqlStatement.ToString(SqlFormatInfo.Minified);

        /// <summary>Gets a <see cref="string"/> representation of the SQL statement.</summary>
        /// <param name="sqlStatement">
        /// The SQL statement to represent as <see cref="string"/>.
        /// </param>
        /// <param name="formatInfo">
        /// The format info to use.
        /// </param>
        /// <remarks>
        /// Uses the <see cref="SqlBuilderPool"/> to minimize <see cref="SqlBuilder"/>
        /// (and by that <see cref="System.Text.StringBuilder"/>) construction and
        /// destruction.
        /// </remarks>
        public static string ToString(this ISqlStatement sqlStatement, SqlFormatInfo formatInfo)
        {
            var builder = SqlBuilder.Pool.Pop();
            builder.FormatInfo = formatInfo;
            sqlStatement.Write(builder);
            var sql = builder.ToString();
            SqlBuilder.Pool.Push(builder);
            return sql;
        }

        /// <summary>Parenthesizes the SQL statement.</summary>
        public static Parenthesis Parenthesize(this ISqlStatement sqlStatement) => new Parenthesis(sqlStatement);

        /// <summary>Helper that can be used to override <see cref="object.ToString()"/>
        /// using <see cref="ISqlStatement.Write(SqlBuilder, int)"/>.
        /// </summary>
        /// <param name="sqlStatement">
        /// The SQL statement to represent as <see cref="string"/>.
        /// </param>
        /// <remarks>
        /// TYpical usage should be:
        /// <code>
        /// public override string ToString() => this.UseSqlBuilder();
        /// </code>
        /// </remarks>
        internal static string UseSqlBuilder(this ISqlStatement sqlStatement) => ToString(sqlStatement, SqlFormatInfo.Debugger);

        internal static string GetDebuggerDisplay(this ISqlStatement sqlStatement) => $"{{{sqlStatement.GetType().Name}}} {ToString(sqlStatement, SqlFormatInfo.Debugger)}";
    }
}
