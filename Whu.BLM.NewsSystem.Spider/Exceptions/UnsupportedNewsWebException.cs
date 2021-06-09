using System;
using System.Runtime.Serialization;

namespace Whu.BLM.NewsSystem.Spider.Exceptions
{
    public class UnsupportedNewsWebException: BaseSpiderException
    {
        public UnsupportedNewsWebException()
        {
        }

        protected UnsupportedNewsWebException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public UnsupportedNewsWebException(string? message) : base(message)
        {
        }

        public UnsupportedNewsWebException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}