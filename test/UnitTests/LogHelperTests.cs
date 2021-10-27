using System;
using Common;
using Common.Exceptions;
using Common.LogHelpers;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace UnitTests
{
    public class LogHelperTests
    {
        [Fact]
        public void LogMessage_Should_Throw_Exception_When_EventId_DefaultValue()
        {
            var mockLogger = new Mock<ILogger>();
            Assert.Throws<UnknownEventIdException>(() => LogHelper.LogMessage(mockLogger.Object, LogLevel.None, default, string.Empty));
        }

        [Fact]
        public void LogMessage_Should_Throw_Exception_When_EventId_NotFound()
        {
            var mockLogger = new Mock<ILogger>();
            Assert.Throws<UnknownEventIdException>(() => LogHelper.LogMessage(mockLogger.Object, LogLevel.None, int.MaxValue, string.Empty));
        }

        [Fact]
        public void LogMessage_Should_Throw_Exception_When_ExceptionArgument_Is_Of_BaseException_Type()
        {
            var mockLogger = new Mock<ILogger>();
            Assert.Throws<BaseExceptionLoggingException>(() => LogHelper.LogMessage(mockLogger.Object, LogLevel.None, 1, string.Empty, new Exception()));
        }
    }
}