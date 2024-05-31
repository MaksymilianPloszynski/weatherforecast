using System.Globalization;
using Newtonsoft.Json;
using Weather.Forecast.Infrastructure.Clients.Abstractions;
using Weather.Forecast.Infrastructure.Clients.Models;

namespace Weather.Forecast.Infrastructure.Clients;

public class WeatherClient : IWeatherClient
{
    private readonly HttpClient _httpClient;
    private readonly CultureInfo _customCulture;

    public WeatherClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _customCulture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
        _customCulture.NumberFormat.NumberDecimalSeparator = ".";
    }
    
    public async Task<ExternalWeather?> GetWeatherAsync(double latitude, double longitude)
    {
        var latitudeValue = latitude.ToString(_customCulture);
        var longitudeValue = longitude.ToString(_customCulture);
        
        var url = $"?latitude={latitudeValue}&longitude={longitudeValue}&hourly=temperature_2m";
        
        var response = await _httpClient.GetAsync(url);
        
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ExternalWeather>(content);
        }
        
        throw new HttpRequestException("Failed to retrieve weather data");
    }
}

