using System.Net;

namespace Common.Exceptions
{
    public sealed class InvalidPhoneNumberException : EuroslackpotException
    {
        public InvalidPhoneNumberException(string message)
            : base(message) => Message = message;

        public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public override string Message { get; set; }
    }
}
