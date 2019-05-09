using System;
using System.Diagnostics;

namespace SqlBob
{
    internal static class Guard
    {
        [DebuggerStepThrough]
        public static T NotNull<T>(T parameter, string paramName) where T : class
        {
            if(parameter is null)
            {
                throw new ArgumentNullException(paramName);
            }
            return parameter;
        }

        [DebuggerStepThrough]
        public static string NotNullOrEmpty([ValidatedNotNull]string parameter, string paramName)
        {
            NotNull(parameter, paramName);
            if (string.Empty == parameter)
            {
                throw new ArgumentException("Argument cannot be an empty string.", paramName);
            }
            return parameter;
        }

        [AttributeUsage(AttributeTargets.Parameter)]
        sealed class ValidatedNotNullAttribute : Attribute { }
    }
}
