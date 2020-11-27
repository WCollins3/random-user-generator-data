using System;


namespace RandomUserGeneratorData.Core.Exceptions
{
    public class UnexpectedApiException : Exception
    {
        public UnexpectedApiException()
        {
        }

        public UnexpectedApiException(string message)
            : base(message)
        {
        }

        public UnexpectedApiException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
