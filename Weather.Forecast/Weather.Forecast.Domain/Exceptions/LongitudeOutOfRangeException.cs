namespace Weather.Forecast.Domain.Exceptions;

public class LongitudeOutOfRangeException : DomainException
{
    public LongitudeOutOfRangeException()
    {
    }

    public LongitudeOutOfRangeException(string message)
        : base(message)
    {
    }

    public LongitudeOutOfRangeException(string message, Exception inner)
        : base(message, inner)
    {
    }
}