namespace Weather.Forecast.API.Responses;

public class WeatherLocationResponse
{
    public int Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public List<WeatherForecastResponse> WeatherForecasts { get; set; } = new();
}