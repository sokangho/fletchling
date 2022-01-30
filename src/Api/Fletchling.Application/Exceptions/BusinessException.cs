using System;
using System.Net;

namespace Fletchling.Application.Exceptions
{
    public class BusinessException : Exception
    {
        public HttpStatusCode StatusCode { get; private set; }

        public BusinessException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}