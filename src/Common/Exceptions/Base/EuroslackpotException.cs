using System;
using System.Net;

namespace Common.Exceptions.Base
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

        public abstract string Message { get; set; }
    }
}
