using System;

namespace Fletchling.Application.Exceptions
{
    public class DataNotFoundException : Exception
    {
        public DataNotFoundException(string message) : base(message)
        {
        }

        public DataNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}