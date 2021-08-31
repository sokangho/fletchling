using System;

namespace Fletchling.Data.Exceptions
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
