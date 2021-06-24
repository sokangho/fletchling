using System;
using System.Net;

namespace Fletchling.Api.Exceptions
{
    public class HttpStatusCodeException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }
        public string ContentType { get; private set; } = "application/json";

        public HttpStatusCodeException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
