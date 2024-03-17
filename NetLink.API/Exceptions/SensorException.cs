namespace NetLink.API.Exceptions
{
    public class SensorException : Exception
    {
        public SensorException()
        {
        }

        public SensorException(string message) : base(message)
        {
        }

        public override string ToString()
        {
            return $"NotFoundException: {Message}";
        }
    }
}
