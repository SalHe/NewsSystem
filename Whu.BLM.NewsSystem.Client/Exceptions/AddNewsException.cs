using System;
using System.Runtime.Serialization;

namespace Whu.BLM.NewsSystem.Client.Exceptions
{
    public class AddNewsException : Exception
    {
        public AddNewsException()
        {
        }

        protected AddNewsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public AddNewsException(string? message) : base(message)
        {
        }

        public AddNewsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}