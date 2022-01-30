using System;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests
{
    public static class TestHelper
    {
        public static Mock<ILogger<T>> VerifyLogging<T>(this Mock<ILogger<T>> logger, LogLevel logLevel,
            string expectedMessage = null)
        {
            Func<object, Type, bool> state = (v, t) =>
            {
                if (expectedMessage != null)
                {
                    return string
                        .Compare(v.ToString(), expectedMessage, StringComparison.Ordinal) == 0;
                }
                else
                {
                    return true;
                }
            };

            logger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == logLevel),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => state(v, t)),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)));

            return logger;
        }
    }
}