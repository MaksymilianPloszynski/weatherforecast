using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Weather.Forecast.Domain.Entities;
using Weather.Forecast.API.Services.Abstractions;
using Weather.Forecast.API.Services.Models;
using Weather.Forecast.Infrastructure.Clients.Abstractions;
using Weather.Forecast.Infrastructure.Data;

namespace Weather.Forecast.API.Services;

public class WeatherForecastService(WeatherForecastDbContext dbContext, IWeatherClient weatherClient, IMapper mapper, ILogger<WeatherForecastService> logger)
    : IWeatherForecastService
{
    public async Task<WeatherLocationModel?> GetForecast(int locationId)
    {
        var location = await dbContext.Locations
            .Include(x => x.WeatherForecasts)
            .SingleOrDefaultAsync(x => x.Id == locationId);

        if (location is not null)
        {
            await FillWithActualWeatherForecast(location);
        }
        
        return mapper.Map<WeatherLocationModel>(location) ?? null;
    }

    private async Task FillWithActualWeatherForecast(Location location)
    {
        try
        {
            var weatherForecast = await weatherClient.GetWeatherAsync(location.Latitude, location.Longitude);
            if (weatherForecast != null)
            {
                location.AssignForecasts(mapper.Map<List<WeatherForecast>>(weatherForecast));
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to retrieve weather data.");
        }
    }
}