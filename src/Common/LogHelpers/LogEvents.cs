using Microsoft.Extensions.Logging;

namespace Common.LogHelpers
{
    public static class LogEvents
    {
        public static EventId GetPlayerEvent = new(1, nameof(GetPlayerEvent));
    }
}