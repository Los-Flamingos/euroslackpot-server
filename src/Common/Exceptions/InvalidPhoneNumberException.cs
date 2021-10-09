using System.Net;

namespace Common.Exceptions
{
    public class InvalidPhoneNumberException : EuroslackpotException
    {
        public InvalidPhoneNumberException(string message)
            : base(message)
        {
        }

        public override HttpStatusCode StatusCode { get; } = HttpStatusCode.BadRequest;
    }
}
