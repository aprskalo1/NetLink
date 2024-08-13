namespace NetLink.API.Exceptions;

internal class DeveloperException(string message) : NetLinkCustomException(message)
{
    public override string ToString()
    {
        return $"DevTokenException: {Message}";
    }
}

internal class EndUserException(string message) : NetLinkCustomException(message)
{
    public override string ToString()
    {
        return $"EndUserException: {Message}";
    }
}

internal class SensorException(string message) : NetLinkCustomException(message)
{
    public override string ToString()
    {
        return $"SensorException: {Message}";
    }
}

internal class NotFoundException(string message) : NetLinkCustomException(message)
{
    public override string ToString()
    {
        return $"NotFoundException: {Message}";
    }
}