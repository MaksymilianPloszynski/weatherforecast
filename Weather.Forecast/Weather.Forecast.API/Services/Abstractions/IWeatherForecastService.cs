using Weather.Forecast.API.Services.Models;

namespace Weather.Forecast.API.Services.Abstractions;

public interface IWeatherForecastService
{
    Task<WeatherLocationModel?> GetForecast(int locationId);
}