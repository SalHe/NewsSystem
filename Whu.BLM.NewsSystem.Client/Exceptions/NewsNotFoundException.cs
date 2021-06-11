using System;
using System.Runtime.Serialization;

namespace Whu.BLM.NewsSystem.Client.Exceptions
{
    public class NewsNotFoundException : Exception
    {
        public NewsNotFoundException()
        {
        }

        protected NewsNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public NewsNotFoundException(string? message) : base(message)
        {
        }

        public NewsNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}