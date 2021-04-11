using System;

namespace Games.Core.Exceptions
{
    public class ValidationException : Exception
    {
        public Error Error { get; set; }
        public ValidationException(Error error) : base()
        {
            Error = error;
        }
        public ValidationException(string message) : base(message)
        {
        }
    }

    public enum Error
    {
        NotFound,
        CategoryNotExists,
        AlreadyExists,
    }
}
