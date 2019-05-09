namespace SqlBob
{
    /// <summary>Represents a SQL JOIN.</summary>
    /// <typeparam name="T">
    /// The type of the join.
    /// </typeparam>
    public abstract class Join<T> : Join where T: Join<T>
    {
        /// <inherritdoc/>
        protected Join(object expression) 
            : base(expression) { }

        /// <summary>Sets the AS alias.</summary>
        public T As(Alias alias)
        {
            Alias = alias;
            return (T)this;
        }

        /// <summary>Sets the ON condition</summary>
        public T On(object condition)
        {
            Condition = Convert(condition) ?? SyntaxError.JoinOnConditionNotSpecified;
            return (T)this;
        }
    }
}
