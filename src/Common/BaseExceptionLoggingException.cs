using System;

namespace Common
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1032:Implement standard exception constructors", Justification = "Exception has strict use case, and does require standard constructors")]
    public class BaseExceptionLoggingException : Exception
    {
        public BaseExceptionLoggingException()
            :base("Custom log messages should not use base exception type")
        {
        }
    }
}
