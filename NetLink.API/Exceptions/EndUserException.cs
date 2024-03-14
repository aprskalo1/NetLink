using System.Runtime.Serialization;

namespace NetLink.API.Exceptions
{
    internal class EndUserException : Exception
    {
        public EndUserException()
        {
        }

        public EndUserException(string? message) : base(message)
        {
        }

        public override string ToString()
        {
            return $"EndUserException: {Message}";
        }
    }
}