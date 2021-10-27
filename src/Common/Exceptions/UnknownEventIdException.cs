using System;
using Microsoft.Extensions.Logging;

namespace Common.Exceptions
{
    public class UnknownEventIdException : Exception
    {
        public UnknownEventIdException(EventId eventId)
            :base($"Event {eventId} was not found")
        {
        }
    }
}