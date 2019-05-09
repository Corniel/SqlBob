using SqlBob.Formatting;
using System;
using System.Diagnostics;

namespace SqlBob
{
    /// <summary>Represent a SQL keyword parameter.</summary>
    [DebuggerDisplay("{DebuggerDisplay}")]
    public struct Parameter : ISqlStatement, IEquatable<Parameter>
    {
        /// <summary>Creates a new instance of a <see cref="Parameter"/>.</summary>
        public Parameter(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                _value = null;
            }
            else
            {
                _value = value[0] == '@'
                    ? value
                    : '@' + value;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly string _value;

        /// <summary>Implicitly casts a <see cref="string"/> to a <see cref="Parameter"/>.</summary>
        public static implicit operator Parameter(string value) => new Parameter(value);

        /// <inherritdoc/>
        public void Write(SqlBuilder builder, int depth = 0)
        {
            Guard.NotNull(builder, nameof(builder));

            builder.Literal(_value);
        }

        /// <summary>Represents the <see cref="Parameter"/> as <see cref="string"/>.</summary>
        public override string ToString() => this.UseSqlBuilder();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string DebuggerDisplay => this.GetDebuggerDisplay();

        /// <inherritdoc/>
        public override bool Equals(object obj) => obj is Parameter other && Equals(other);

        /// <inherritdoc/>
        public bool Equals(Parameter other) => string.Equals(_value, other._value, StringComparison.InvariantCultureIgnoreCase);

        /// <inherritdoc/>
        public override int GetHashCode() => _value is null ? 0 : _value.ToUpperInvariant().GetHashCode();
    }
}
