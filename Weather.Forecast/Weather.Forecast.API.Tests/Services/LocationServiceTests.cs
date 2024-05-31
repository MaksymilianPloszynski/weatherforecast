using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Weather.Forecast.API.Mappers;
using Weather.Forecast.API.Services;
using Weather.Forecast.API.Services.Models;
using Weather.Forecast.Domain.Entities;
using Weather.Forecast.Infrastructure.Data;

namespace Weather.Forecast.API.Tests.Services;

    [TestFixture]
    public class LocationServiceTests
    {
        private IMapper _mapper;
        private DbContextOptions<WeatherForecastDbContext> _dbContextOptions;
        private WeatherForecastDbContext _dbContext;
        private LocationService _locationService;

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<WeatherForecastDbContext>()
                .UseInMemoryDatabase(databaseName: "WeatherForecastTestDb")
                .Options;
            _dbContext = new WeatherForecastDbContext(_dbContextOptions);

            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new WeatherProfile()); });
            _mapper = mappingConfig.CreateMapper();
            
            _locationService = new LocationService(_dbContext, _mapper);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }

        [Test]
        public async Task GetAllLocations_ShouldReturnPagedResult()
        {
            // Arrange
            var locations = new List<Location>
            {
                new Location(50.0, 10.0),
                new Location(51.0, 11.0),
                new Location(52.0, 12.0)
            };
            await _dbContext.Locations.AddRangeAsync(locations);
            await _dbContext.SaveChangesAsync();

            var locationInfoModels = locations.Select(l => new LocationInfoModel
            {
                Latitude = l.Latitude,
                Longitude = l.Longitude
            }).ToList();
            
            // Act
            var result = await _locationService.GetAllLocations(1, 2);

            // Assert
            result.Data.Should().HaveCount(2);
            result.Pagination.TotalItems.Should().Be(3);
            result.Pagination.CurrentPage.Should().Be(1);
            result.Pagination.PageSize.Should().Be(2);
        }

        [Test]
        public async Task AddLocation_ShouldAddLocation()
        {
            // Arrange
            var locationInfoModel = new LocationInfoModel
            {
                Latitude = 50.0,
                Longitude = 10.0
            };

            // Act
            var result = await _locationService.AddLocation(50.0, 10.0);

            // Assert
            result.Should().NotBeNull();
            result.Latitude.Should().Be(50.0);
            result.Longitude.Should().Be(10.0);

            var locationInDb = await _dbContext.Locations.FirstOrDefaultAsync(l => l.Latitude == 50.0 && l.Longitude == 10.0);
            locationInDb.Should().NotBeNull();
        }

        [Test]
        public async Task DeleteLocation_ShouldRemoveLocation()
        {
            // Arrange
            var location = new Location(50.0, 10.0);
            await _dbContext.Locations.AddAsync(location);
            await _dbContext.SaveChangesAsync();

            // Act
            await _locationService.DeleteLocation(location.Id);

            // Assert
            var locationInDb = await _dbContext.Locations.FindAsync(location.Id);
            locationInDb.Should().BeNull();
        }
    }