using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Weather.Forecast.Domain.Entities;

namespace Weather.Forecast.Infrastructure.Data.Configurations;

public class LocationConfiguration : BaseEntityConfiguration<Location>
{
    public override void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.Property(x => x.Latitude);
        
        builder.Property(x => x.Longitude);

        builder
            .HasMany(x => x.WeatherForecasts)
            .WithOne(x => x.Location)
            .HasForeignKey(x => x.LocationId);
        
        base.Configure(builder);
    }
}     