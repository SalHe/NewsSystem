using System;

namespace Whu.BLM.NewsSystem.Client.Exceptions
{
    public class LoginErrorException: Exception
    {
        public string Reason { get; set; }

        public LoginErrorException(string reason) : base(reason)
        {
            Reason = reason;
        }
    }
}