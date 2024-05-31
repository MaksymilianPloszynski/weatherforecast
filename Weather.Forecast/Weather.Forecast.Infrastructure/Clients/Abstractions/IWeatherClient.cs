using Weather.Forecast.Infrastructure.Clients.Models;

namespace Weather.Forecast.Infrastructure.Clients.Abstractions;

public interface IWeatherClient
{
    Task<ExternalWeather?> GetWeatherAsync(double latitude, double longitude);
}