using System;


namespace RandomUserGeneratorData.Core.Exceptions
{
    /// <summary>
    ///     Exception thrown when the random user API returns an unexpected result.
    /// </summary>
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
