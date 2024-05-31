namespace Weather.Forecast.Domain.Exceptions;

public class InvalidWeatherForecastsException : DomainException
{
    public InvalidWeatherForecastsException()
    {
    }

    public InvalidWeatherForecastsException(string message)
        : base(message)
    {
    }

    public InvalidWeatherForecastsException(string message, Exception inner)
        : base(message, inner)
    {
    }
}