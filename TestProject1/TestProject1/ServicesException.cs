using System;

namespace TestProject1
{
    public class ServicesException : Exception
    {
        public ServicesException(int code, string message) : base(message)
        {
            Code = code;
        }
        public int Code { get; set; }
    }
}
