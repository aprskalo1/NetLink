namespace NetLink.API.Exceptions
{
    internal class DevTokenException : Exception
    {
        public DevTokenException()
        {
        }

        public DevTokenException(string message) : base(message)
        {
        }

        public override string ToString()
        {
            return $"DevTokenException: {Message}";
        }
    }
}
