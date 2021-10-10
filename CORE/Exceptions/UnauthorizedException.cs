using System;

namespace CORE.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message = default) : base(message)
        {
        }
    }
}