namespace Weather.Forecast.Domain.Exceptions;

public class InvalidTemperatureException : DomainException
{
    public InvalidTemperatureException()
    {
    }

    public InvalidTemperatureException(string message)
        : base(message)
    {
    }

    public InvalidTemperatureException(string message, Exception inner)
        : base(message, inner)
    {
    }
}