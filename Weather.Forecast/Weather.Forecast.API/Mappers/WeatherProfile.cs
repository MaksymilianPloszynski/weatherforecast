using AutoMapper;
using Weather.Forecast.API.Responses;
using Weather.Forecast.API.Services.Models;
using Weather.Forecast.Domain.Entities;
using Weather.Forecast.Infrastructure.Clients.Models;

namespace Weather.Forecast.API.Mappers;

public class WeatherProfile : Profile
{
    public WeatherProfile()
    {
        CreateMap<Location, WeatherLocationModel>(); 
        
        CreateMap<Location, LocationInfoModel>(); 
        
        CreateMap<WeatherForecast, WeatherForecastModel>();
        
        CreateMap<ExternalWeather, List<WeatherForecast>>()
            .ConvertUsing(source => ConvertToWeatherForecastEntity(source));
        
        CreateMap<WeatherLocationModel, WeatherLocationResponse>();
        
        CreateMap<LocationInfoModel, LocationInfoResponse>();
        
        CreateMap<WeatherForecastModel, WeatherForecastResponse>();
    }

    private List<WeatherForecast> ConvertToWeatherForecastEntity(ExternalWeather model)
    {
        var forecasts = new List<WeatherForecast>();

        if (model?.Hourly?.Time is null || model.Hourly.Temperature2M is null)
        {
            return forecasts;
        }

        for (int i = 0; i < model.Hourly.Time.Count; i++)
        {
            var dateTime = DateTime.TryParse(model.Hourly.Time[i], out var parsedDate) ? parsedDate : (DateTime?)null;
            if (dateTime.HasValue)
            {
                var temperature = Convert.ToInt32(model.Hourly.Temperature2M[i]);
                forecasts.Add(new WeatherForecast(dateTime.Value, temperature));
            }
        }

        return forecasts;
    }
}