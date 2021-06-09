using System;
using System.Runtime.Serialization;

namespace Whu.BLM.NewsSystem.Spider.Exceptions
{
    public class BaseSpiderException : Exception
    {
        public BaseSpiderException()
        {
        }

        protected BaseSpiderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public BaseSpiderException(string? message) : base(message)
        {
        }

        public BaseSpiderException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}