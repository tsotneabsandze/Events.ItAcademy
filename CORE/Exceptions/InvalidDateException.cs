using System;

namespace CORE.Exceptions
{
    public class InvalidDateException : Exception
    {
        public InvalidDateException(string message = default) : base(message)
        {
        }
    }
}