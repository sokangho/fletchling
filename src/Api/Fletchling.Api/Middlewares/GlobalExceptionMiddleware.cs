using Fletchling.Api.Models;
using Fletchling.Api.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Fletchling.Api.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                await HandleHttpStatusCodeExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        // Handle caught exceptions
        private Task HandleHttpStatusCodeExceptionAsync(HttpContext context, BusinessException ex)
        {
            var response = new ErrorResponse
            {
                StatusCode = (int)ex.StatusCode,
                ErrorMessage = ex.Message
            };

            context.Response.StatusCode = response.StatusCode;
            context.Response.ContentType = ex.ContentType;

            return context.Response.WriteAsJsonAsync(response);
        }

        // Handle uncaught exception with a default 500 status code and a default error message
        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var response = new ErrorResponse
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                ErrorMessage = "Internal server error."
            };

            context.Response.StatusCode = response.StatusCode;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
