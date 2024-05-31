using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Weather.Forecast.API.Mappers;
using Weather.Forecast.API.Services;
using Weather.Forecast.API.Services.Models;
using Weather.Forecast.Domain.Entities;
using Weather.Forecast.Infrastructure.Clients.Abstractions;
using Weather.Forecast.Infrastructure.Clients.Models;
using Weather.Forecast.Infrastructure.Data;

namespace Weather.Forecast.API.Tests.Services;

    [TestFixture]
    public class WeatherForecastServiceTests
    {
        private Mock<IWeatherClient> _weatherClientMock;
        private IMapper _mapper;
        private Mock<ILogger<WeatherForecastService>> _loggerMock;
        private DbContextOptions<WeatherForecastDbContext> _dbContextOptions;
        private WeatherForecastDbContext _dbContext;
        private WeatherForecastService _weatherForecastService;

        [SetUp]
        public void Setup()
        {
            _weatherClientMock = new Mock<IWeatherClient>();
            _loggerMock = new Mock<ILogger<WeatherForecastService>>();
            _dbContextOptions = new DbContextOptionsBuilder<WeatherForecastDbContext>()
                .UseInMemoryDatabase(databaseName: "WeatherForecastTestDb")
                .Options;
            _dbContext = new WeatherForecastDbContext(_dbContextOptions);

            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new WeatherProfile()); });
            _mapper = mappingConfig.CreateMapper();
            
            _weatherForecastService = new WeatherForecastService(_dbContext, _weatherClientMock.Object, _mapper, _loggerMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [Test]
        public async Task GetForecast_ShouldReturnWeatherLocationModel_WhenLocationExists()
        {
            // Arrange
            var location = new Location(50.0, 10.0);
            await _dbContext.Locations.AddAsync(location);
            await _dbContext.SaveChangesAsync();

            var weatherLocationModel = new WeatherLocationModel()
            {
                Id = location.Id,
                Latitude = location.Latitude,
                Longitude = location.Longitude
            };
            
            // Act
            var result = await _weatherForecastService.GetForecast(location.Id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(weatherLocationModel);
        }

        [Test]
        public async Task GetForecast_ShouldReturnNull_WhenLocationDoesNotExist()
        {
            // Act
            var result = await _weatherForecastService.GetForecast(1);

            // Assert
            result.Should().BeNull();
        }

        [Test]
        public async Task FillWithActualWeatherForecast_ShouldAssignForecasts_WhenWeatherClientReturnsData()
        {
            // Arrange
            var location = new Location(50.0, 10.0);
            await _dbContext.Locations.AddAsync(location);
            await _dbContext.SaveChangesAsync();


            var weather = new WeatherLocationModel
            {
                Id = location.Id,
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                WeatherForecasts = new List<WeatherForecastModel>
                {
                    new WeatherForecastModel
                    {
                        Time = Convert.ToDateTime("2024-05-25T07:00"),
                        TemperatureC = 10d
                    },
                    new WeatherForecastModel
                    {
                        Time = Convert.ToDateTime("2024-05-25T08:00"),
                        TemperatureC = 12d
                    },
                    new WeatherForecastModel
                    {
                        Time = Convert.ToDateTime("2024-05-25T09:00"),
                        TemperatureC = 21d
                    },
                }
            };
            
            _weatherClientMock.Setup(wc => wc.GetWeatherAsync(It.IsAny<double>(), It.IsAny<double>()))
                .ReturnsAsync(new ExternalWeather
                {
                    Latitude = location.Latitude,
                    Longitude = location.Latitude,
                    Hourly = new ExternalWeather.ExternalHourlyData
                    {
                        Time =
                        {
                            "2024-05-25T07:00", "2024-05-25T08:00", "2024-05-25T09:00"
                        },
                        Temperature2M = [10d, 12d, 21d]
                    }
                });

            // Act
            var result = await _weatherForecastService.GetForecast(location.Id);

            // Assert
            result.Should().BeEquivalentTo(weather);
        }
    }