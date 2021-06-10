using System;
using System.Runtime.Serialization;

namespace Whu.BLM.NewsSystem.Client.Exceptions
{
    public class RegisterException : Exception
    {
        public RegisterException()
        {
        }

        protected RegisterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public RegisterException(string? message) : base(message)
        {
        }

        public RegisterException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}