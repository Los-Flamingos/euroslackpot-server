using System;

namespace Common.Exceptions
{
    public class BaseExceptionLoggingException : Exception
    {
        public BaseExceptionLoggingException()
            :base("Custom log messages should not use base exception type")
        {
        }
    }
}
