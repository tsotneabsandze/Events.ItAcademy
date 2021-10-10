using System;

namespace CORE.Exceptions
{
    public class IdentifierMismatchException : Exception
    {
        public IdentifierMismatchException(string message) : base(message)
        {
        }
    }
}