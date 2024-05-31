using Microsoft.EntityFrameworkCore;
using Weather.Forecast.Domain.Entities;
using Weather.Forecast.Infrastructure.Data.Configurations;

namespace Weather.Forecast.Infrastructure.Data;

public class WeatherForecastDbContext : DbContext
{
    public WeatherForecastDbContext(DbContextOptions<WeatherForecastDbContext> options) : base(options)
    {
    }

    public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    public DbSet<Location> Locations { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new LocationConfiguration());
        modelBuilder.ApplyConfiguration(new WeatherForecastConfiguration());

        base.OnModelCreating(modelBuilder);
    }
    
    public void Migrate()
    {
        Database.Migrate();
    }
}