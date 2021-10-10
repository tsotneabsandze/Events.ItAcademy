using System;

namespace CORE.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string message = default) : base(message)
        {
        }
    }
}