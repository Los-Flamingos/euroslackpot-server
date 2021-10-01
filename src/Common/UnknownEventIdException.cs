using System;
using Microsoft.Extensions.Logging;

namespace Common
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1032:Implement standard exception constructors", Justification = "Exception has strict use case, and does require standard constructors")]
    public class UnknownEventIdException : Exception
    {
        public UnknownEventIdException(EventId eventId)
            :base($"Event {eventId} was not found")
        {
        }
    }
}