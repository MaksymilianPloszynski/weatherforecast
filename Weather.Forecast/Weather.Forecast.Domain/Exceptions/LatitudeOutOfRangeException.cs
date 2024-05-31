namespace Weather.Forecast.Domain.Exceptions;

public class LatitudeOutOfRangeException : DomainException
{
    public LatitudeOutOfRangeException()
    {
    }

    public LatitudeOutOfRangeException(string message)
        : base(message)
    {
    }

    public LatitudeOutOfRangeException(string message, Exception inner)
        : base(message, inner)
    {
    }
}