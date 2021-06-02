using System;

namespace API.Utils.Exceptions
{
    public class APIException : Exception
    {
        public APIException(string message) : base(message) { }
    }
}