using FluentAssertions;
using Weather.Forecast.Domain.Entities;
using Weather.Forecast.Domain.Exceptions;

namespace Weather.Forecast.Domain.Test.Entities;

[TestFixture]
public class LocationTests
{
    [Test]
    public void Constructor_WithValidLatitudeAndLongitude_ShouldCreateLocation()
    {
        // Arrange
        double latitude = 50.0;
        double longitude = 10.0;

        // Act
        var location = new Location(latitude, longitude);

        // Assert
        location.Latitude.Should().Be(latitude);
        location.Longitude.Should().Be(longitude);
    }

    [Test]
    public void Constructor_WithInvalidLatitude_ShouldThrowLatitudeOutOfRangeException()
    {
        // Arrange
        double invalidLatitude = 100.0;
        double longitude = 10.0;

        // Act
        Action act = () => new Location(invalidLatitude, longitude);

        // Assert
        act.Should().Throw<LatitudeOutOfRangeException>()
           .WithMessage("Latitude must be between -90 and 90 degrees.");
    }

    [Test]
    public void Constructor_WithInvalidLongitude_ShouldThrowLatitudeOutOfRangeException()
    {
        // Arrange
        double latitude = 50.0;
        double invalidLongitude = 200.0;

        // Act
        Action act = () => new Location(latitude, invalidLongitude);

        // Assert
        act.Should().Throw<LatitudeOutOfRangeException>()
           .WithMessage("Longitude must be between -180 and 180 degrees.");
    }

    [Test]
    public void AssignForecasts_WithValidWeatherForecasts_ShouldAssignForecasts()
    {
        // Arrange
        var location = new Location(50.0, 10.0);
        var weatherForecasts = new List<WeatherForecast>
        {
            new WeatherForecast(DateTime.UtcNow, 25),
            new WeatherForecast(DateTime.UtcNow.AddDays(1), 28)
        };

        // Act
        location.AssignForecasts(weatherForecasts);

        // Assert
        location.WeatherForecasts.Should().BeEquivalentTo(weatherForecasts);
    }

    [Test]
    public void AssignForecasts_WithNullWeatherForecasts_ShouldThrowInvalidWeatherForecastsException()
    {
        // Arrange
        var location = new Location(50.0, 10.0);

        // Act
        Action act = () => location.AssignForecasts(null);

        // Assert
        act.Should().Throw<InvalidWeatherForecastsException>()
           .WithMessage("Weather forecasts collection cannot be null.");
    }
}