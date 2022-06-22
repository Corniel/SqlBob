namespace SqlBob.Formatting
{
    /// <summary>The SQL version.</summary>
    public enum SqlVersion
    {
        /// <summary>Generic (default).</summary>
        Generic = 0,
        
        /// <summary>SQL Server 2008.</summary>
        SqlServer2008,

        /// <summary>SQL Server 2012.</summary>
        SqlServer2012,

        /// <summary>SQL Server 2014.</summary>
        SqlServer2014,

        /// <summary>SQL Server 2016.</summary>
        SqlServer2016,

        /// <summary>SQL Server 2017.</summary>
        SqlServer2017,
    }
}
