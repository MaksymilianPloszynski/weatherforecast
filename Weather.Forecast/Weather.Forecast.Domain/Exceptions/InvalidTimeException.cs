namespace Weather.Forecast.Domain.Exceptions;

public class InvalidTimeException : DomainException
{
    public InvalidTimeException()
    {
    }

    public InvalidTimeException(string message)
        : base(message)
    {
    }

    public InvalidTimeException(string message, Exception inner)
        : base(message, inner)
    {
    }
}