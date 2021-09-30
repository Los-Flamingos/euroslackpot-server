using System;
using System.Net;

namespace Common.Exceptions
{
    public abstract class EuroslackpotException : Exception
    {
        protected EuroslackpotException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected EuroslackpotException(string message)
            : base(message) 
        {
        }

        protected EuroslackpotException()
        {
        }

        public abstract HttpStatusCode StatusCode { get; }
    }
}
