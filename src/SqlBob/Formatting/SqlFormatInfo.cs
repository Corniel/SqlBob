namespace SqlBob.Formatting
{
    /// <summary>Represents SQL formatting information.</summary>
    public class SqlFormatInfo
    {
        internal static readonly SqlFormatInfo Minified = new SqlFormatInfo { UseNewLine = false, Indent = "", Throw = true };
        internal static readonly SqlFormatInfo Debugger = new SqlFormatInfo();

        /// <summary>Gets and sets the SQL version.</summary>
        public SqlVersion Version { get; set; }

        /// <summary>Use new line (default), or spaces to split statements.</summary>
        public bool UseNewLine { get; set; } = true;

        /// <summary>Use upper case for keywords (default).</summary>
        public bool UseUpperCase { get; set; } = true;

        /// <summary>The indent to apply (when UseNewLine = true).</summary>
        public string Indent { get; set; } = new string(' ', 4);

        /// <summary>Only to support <see cref="Debugger"/>.</summary>
        internal bool Throw { get; set; }
    }
}
