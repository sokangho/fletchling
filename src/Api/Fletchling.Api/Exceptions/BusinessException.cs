using System;
using System.Net;

namespace Fletchling.Api.Exceptions
{
    public class BusinessException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }
        public string ContentType { get; private set; } = "application/json";

        public BusinessException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
