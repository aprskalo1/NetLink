namespace NetLink.API.Exceptions
{
    internal class NotFoundException : Exception
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public override string ToString()
        {
            return $"NotFoundException: {Message}";
        }
    }
}
