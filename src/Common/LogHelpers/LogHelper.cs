using System;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Common.LogHelpers
{
    public static class LogHelper
    {
        public static void LogMessage(ILogger logger, LogLevel severity, EventId eventId, string message, Exception exception = null, params object[] arguments)
        {
            if (!EventIdExists(eventId))
            {
                throw new UnknownEventIdException(eventId);
            }

            if (exception == null)
            {
                logger.Log(severity, eventId, message, arguments);
            }

            if (exception?.GetType() == typeof(Exception))
            {
                throw new BaseExceptionLoggingException();
            }

            logger.Log(severity, eventId, message, arguments);
        }

        private static bool EventIdExists(EventId id) => typeof(LogEvents).GetFields().Select(x => x.GetValue(null)).Cast<EventId>().Contains(id);
    }
}