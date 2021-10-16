using System;

namespace CORE.Exceptions
{
    public class InvalidArchivingException : Exception
    {
        public InvalidArchivingException(string message = default)
            : base(message)
        {
        }
    }
}