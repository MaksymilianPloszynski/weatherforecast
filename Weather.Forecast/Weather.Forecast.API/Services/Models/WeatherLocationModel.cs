namespace Weather.Forecast.API.Services.Models;

public class WeatherLocationModel
{
    public int Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public List<WeatherForecastModel> WeatherForecasts { get; set; } = new();
}