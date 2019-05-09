using System;
using System.Runtime.Serialization;

namespace SqlBob
{
    /// <summary>Represents a SQL Syntax exception.</summary>
    [Serializable]
    public class SqlSyntaxException : Exception
    {
        /// <inheritdoc />
        public SqlSyntaxException() { }

        /// <inheritdoc />
        public SqlSyntaxException(string message) : base(message) { }

        /// <inheritdoc />
        public SqlSyntaxException(string message, Exception innerException) : base(message, innerException) { }

        /// <inheritdoc />
        protected SqlSyntaxException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
