using Weather.Forecast.Domain.Exceptions;

namespace Weather.Forecast.Domain.Entities;

public class WeatherForecast : BaseEntity
{
    public int LocationId { get; private set; }
    public virtual Location Location { get; private set; }
    public DateTime Time { get; private set; }
    public double TemperatureC { get; private set; }

    protected WeatherForecast()
    {
        
    }
    
    public WeatherForecast(DateTime time, double temperatureC)
    {
        if (time == default)
        {
            throw new InvalidTimeException("Time must be a valid date.");
        }
        
        if (temperatureC < -100 || temperatureC > 60)
        {
            throw new InvalidTemperatureException("Temperature must be between -100 and 60 degrees Celsius.");
        }

        Time = time;
        TemperatureC = temperatureC;
    }
}