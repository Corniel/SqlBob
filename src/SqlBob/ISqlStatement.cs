using SqlBob.Formatting;

namespace SqlBob
{
    /// <summary>Represents a SQL statement.</summary>
    public interface ISqlStatement
    {
        /// <summary>Writes the SQL statement to the SQL builder buffer.</summary>
        /// <param name="builder">
        /// The SQL builder.
        /// </param>
        /// <param name="depth">
        /// The nested depth.
        /// </param>
        void Write(SqlBuilder builder, int depth = 0);
    }
}
