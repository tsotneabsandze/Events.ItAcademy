using System;

namespace CORE.Exceptions
{
    public class InvalidCrudOperationException : Exception
    {
        public InvalidCrudOperationException(string message = default) : base(message)
        {
            
        }
    }
}