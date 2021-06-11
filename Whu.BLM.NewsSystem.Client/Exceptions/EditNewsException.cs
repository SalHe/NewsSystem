using System;
using System.Runtime.Serialization;

namespace Whu.BLM.NewsSystem.Client.Exceptions
{
    public class EditNewsException: Exception
    {
        public EditNewsException()
        {
        }

        protected EditNewsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public EditNewsException(string? message) : base(message)
        {
        }

        public EditNewsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}