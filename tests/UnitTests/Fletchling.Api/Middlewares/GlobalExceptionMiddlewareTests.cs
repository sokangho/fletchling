using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Fletchling.Api.Middlewares;
using Fletchling.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace UnitTests.Fletchling.Api.Middlewares
{
    public class GlobalExceptionMiddlewareTests
    {
        [Fact]
        public async Task Invoke_WhenBusinessExceptionIsThrown_HandleBusinessException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<GlobalExceptionMiddleware>>();

            var context = new DefaultHttpContext();
            var bodyStream = new MemoryStream();
            context.Response.Body = bodyStream;
            context.Request.Path = "/path";

            var exception = new BusinessException(HttpStatusCode.NotFound, "Not found message");
            RequestDelegate next = (_) => throw exception;
            var middleware = new GlobalExceptionMiddleware(next, loggerMock.Object);

            // Act
            await middleware.Invoke(context);

            var response = await ReadResponse(bodyStream);

            // Assert
            Assert.Equal(404, context.Response.StatusCode);
            Assert.Contains(exception.Message, response);
            loggerMock.VerifyLogging(LogLevel.Warning, exception.Message);
        }
        
        [Fact]
        public async Task Invoke_WhenAnyExceptionIsThrown_HandleException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<GlobalExceptionMiddleware>>();

            var context = new DefaultHttpContext();
            var bodyStream = new MemoryStream();
            context.Response.Body = bodyStream;
            context.Request.Path = "/path";

            var exception = new Exception("Unexpected error");
            RequestDelegate next = (_) => throw exception;
            var middleware = new GlobalExceptionMiddleware(next, loggerMock.Object);

            // Act
            await middleware.Invoke(context);

            var response = await ReadResponse(bodyStream);

            // Assert
            Assert.Equal(500, context.Response.StatusCode);
            Assert.Contains("Internal server error", response);
            loggerMock.VerifyLogging(LogLevel.Error, exception.Message);
        }

        private async Task<string> ReadResponse(MemoryStream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            using var stringReader = new StreamReader(stream);
            var response = await stringReader.ReadToEndAsync();

            return response;
        }
    }
}