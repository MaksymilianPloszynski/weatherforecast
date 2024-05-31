using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Weather.Forecast.Domain.Entities;

namespace Weather.Forecast.Infrastructure.Data.Configurations;

public class WeatherForecastConfiguration : BaseEntityConfiguration<WeatherForecast>
{
    public override void Configure(EntityTypeBuilder<WeatherForecast> builder)
    {
        base.Configure(builder);

        builder
            .Property(x => x.Time);
        
        builder
            .Property(x => x.TemperatureC);
        
        builder
            .HasOne(x => x.Location)
            .WithMany(x => x.WeatherForecasts)
            .HasForeignKey(x => x.LocationId);
    }
}