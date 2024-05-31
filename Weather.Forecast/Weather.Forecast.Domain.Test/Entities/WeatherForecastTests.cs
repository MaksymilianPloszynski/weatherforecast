using FluentAssertions;
using Weather.Forecast.Domain.Entities;
using Weather.Forecast.Domain.Exceptions;

namespace Weather.Forecast.Domain.Test.Entities;

[TestFixture]
public class WeatherForecastTests
{
    [Test]
    public void Constructor_WithValidTimeAndTemperature_ShouldCreateWeatherForecast()
    {
        // Arrange
        var time = DateTime.UtcNow;
        int temperatureC = 25;

        // Act
        var weatherForecast = new WeatherForecast(time, temperatureC);

        // Assert
        weatherForecast.Time.Should().Be(time);
        weatherForecast.TemperatureC.Should().Be(temperatureC);
    }

    [Test]
    public void Constructor_WithInvalidTime_ShouldThrowInvalidTimeException()
    {
        // Arrange
        var invalidTime = default(DateTime);
        int temperatureC = 25;

        // Act
        Action act = () => new WeatherForecast(invalidTime, temperatureC);

        // Assert
        act.Should().Throw<InvalidTimeException>()
            .WithMessage("Time must be a valid date.");
    }

    [Test]
    public void Constructor_WithInvalidTemperature_ShouldThrowInvalidTemperatureException()
    {
        // Arrange
        var time = DateTime.UtcNow;
        int invalidTemperatureC = -150;

        // Act
        Action act = () => new WeatherForecast(time, invalidTemperatureC);

        // Assert
        act.Should().Throw<InvalidTemperatureException>()
            .WithMessage("Temperature must be between -100 and 60 degrees Celsius.");
    }

    [Test]
    public void Constructor_WithTemperatureAboveMax_ShouldThrowInvalidTemperatureException()
    {
        // Arrange
        var time = DateTime.UtcNow;
        int invalidTemperatureC = 70;

        // Act
        Action act = () => new WeatherForecast(time, invalidTemperatureC);

        // Assert
        act.Should().Throw<InvalidTemperatureException>()
            .WithMessage("Temperature must be between -100 and 60 degrees Celsius.");
    }
}