using Weather.Forecast.Domain.Exceptions;

namespace Weather.Forecast.Domain.Entities;

public class Location : BaseEntity
{
    public double Latitude { get; }
    public double Longitude { get; }
    public virtual IReadOnlyList<WeatherForecast> WeatherForecasts => _weatherForecasts;

    private List<WeatherForecast> _weatherForecasts = new();

    protected Location()
    {
        
    }
    
    public Location(double latitude, double longitude)
    {
        if (latitude < -90 || latitude > 90)
        {
            throw new LatitudeOutOfRangeException("Latitude must be between -90 and 90 degrees.");
        }

        if (longitude < -180 || longitude > 180)
        {
            throw new LatitudeOutOfRangeException("Longitude must be between -180 and 180 degrees.");
        }

        Latitude = latitude;
        Longitude = longitude;
    }

    public void AssignForecasts(IEnumerable<WeatherForecast> weatherForecasts)
    {
        _weatherForecasts = weatherForecasts?.ToList() ?? throw new InvalidWeatherForecastsException("Weather forecasts collection cannot be null.");
    }
}