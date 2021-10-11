using System;

namespace CORE.Exceptions
{
    public class ResourceCanNotBeEditedException : Exception
    {
        public ResourceCanNotBeEditedException(string message = default) : base(message)
        {
        }
    }
}