using System;
using System.Text;

namespace SqlBob.Formatting
{
    /// <summary>A <see cref="StringBuilder"/> based SQL (string) builder.</summary>
    public class SqlBuilder
    {
        internal static readonly SqlBuilderPool Pool = new SqlBuilderPool();

        private readonly StringBuilder sb = new StringBuilder();

        /// <summary>Creates a new instance of a <see cref="SqlBuilder"/>.</summary>
        public SqlBuilder() : this(null) { }

        /// <summary>Creates a new instance of a <see cref="SqlBuilder"/>.</summary>
        public SqlBuilder(SqlFormatInfo formatInfo) => FormatInfo = formatInfo;

        /// <summary>Gets and sets the format info.</summary>
        /// <remarks>
        /// To prevent <see cref="NullReferenceException"/>s,
        /// it is not possible to assign this with null.
        /// </remarks>
        public SqlFormatInfo FormatInfo
        {
            get => Info;
            set => Info = value ?? new SqlFormatInfo();
        }
        private SqlFormatInfo Info = new SqlFormatInfo();

        /// <summary>Writes a literal <see cref="string"/>.</summary>
        public SqlBuilder Literal(string value)
        {
            sb.Append(value);
            return this;
        }

        /// <summary>Writes a SQL statement.</summary>
        /// <param name="statement">
        /// The SQL statement to write.
        /// </param>
        /// <param name="depth"></param>
        public SqlBuilder Write(ISqlStatement statement, int depth = 0)
        {
            Guard.NotNull(statement, nameof(statement));
            statement.Write(this, depth);
            return this;
        }

        /// <summary>Concatenates all SQL statements using the specified
        /// separator between each statement. 
        /// </summary>
        /// <param name="separator">
        /// The separator to write.
        /// </param>
        /// <param name="write">
        /// The write action to apply on every statements.
        /// </param>
        /// <param name="statements">
        /// The statements to join.
        /// </param>
        public SqlBuilder Join(object separator, Action<SqlBuilder, ISqlStatement> write, params ISqlStatement[] statements)
        {
            Guard.NotNull(statements, nameof(statements));

            // Nothing to join.
            if (statements.Length == 0)
            {
                return this;
            }
            write(this, statements[0]);
            for (var index = 1; index < statements.Length; index++)
            {
                Write(SqlStatement.Convert(separator));
                write.Invoke(this, statements[index]);
            }
            return this;
        }

        /// <summary>Writes the indent.</summary>
        public SqlBuilder Indent(int depth)
        {
            if (Info.UseNewLine)
            {
                for (var i = 0; i < depth; i++)
                {
                    sb.Append(Info.Indent);
                }
            }
            return this;
        }

        /// <summary>Writes a single space.</summary>
        public SqlBuilder Space()
        {
            if (sb.Length != 0 && sb[sb.Length - 1] != ' ')
            {
                Literal(" ");
            }
            return this;
        }

        /// <summary>Writes a new line or a space depending on the settings.</summary>
        public SqlBuilder NewLineOrSpace()
        {
            if (Info.UseNewLine)
            {
                sb.AppendLine();
            }
            else
            {
                Space();
            }
            return this;
        }

        /// <summary>Writes a new line or a nothing depending on the settings.</summary>
        public SqlBuilder NewLine()
        {
            if (Info.UseNewLine)
            {
                sb.AppendLine();
            }
            return this;
        }

        /// <summary>Returns the constructed SQL as <see cref="string"/>.</summary>
        public override string ToString()
        {
            // Trims the end.
            while (sb.Length != 0 && sb[sb.Length - 1] == ' ')
            {
                sb.Remove(sb.Length - 1, 1);
            }
            SyntaxErrorHandling();

            return sb.ToString();
        }

        /// <summary>Throw when it should throw and has a syntax error.</summary>
        private void SyntaxErrorHandling()
        {
            if (Throw && HasSyntaxError)
            {
                throw new SqlSyntaxException(sb.ToString());
            }
        }

        /// <summary>Shortcut for <see cref="SqlFormatInfo.Throw"/>.</summary>
        internal bool Throw => FormatInfo.Throw;
        internal bool IsDebug => FormatInfo == SqlFormatInfo.Debugger;
        internal int Capacity => sb.Capacity;

        /// <summary>True if an syntax error was rendered.</summary>
        internal bool HasSyntaxError { get; set; }

        /// <summary>Clears the state of the builder.</summary>
        internal void Clear()
        {
            sb.Clear();
            HasSyntaxError = false;
        }
    }
}
