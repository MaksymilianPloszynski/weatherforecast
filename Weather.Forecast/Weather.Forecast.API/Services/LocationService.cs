using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Weather.Forecast.Domain.Entities;
using Weather.Forecast.API.Services.Abstractions;
using Weather.Forecast.API.Services.Models;
using Weather.Forecast.Infrastructure.Data;

namespace Weather.Forecast.API.Services;

public class LocationService : ILocationService
{
    private readonly WeatherForecastDbContext _dbContext;
    private readonly IMapper _mapper;

    public LocationService(WeatherForecastDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<PagedResult<LocationInfoModel>> GetAllLocations(int pageNumber, int pageSize)
    {
        var query = _dbContext.Locations;
        var pageResult = await query
            .OrderBy(x => x.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        var totalCount = await query.CountAsync();
        
        var pagedResult = new PagedResult<LocationInfoModel>(
            _mapper.Map<List<LocationInfoModel>>(pageResult), 
            totalCount, 
            pageNumber, 
            pageSize);
        
        return pagedResult;
    }

    public async Task<LocationInfoModel> AddLocation(double latitude, double longitude)
    {
        var location = new Location(latitude, longitude);
        
        await _dbContext.Locations.AddAsync(location);
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<LocationInfoModel>(location);
    }

    public async Task DeleteLocation(int locationId)
    {
        var location = await _dbContext.Locations.FindAsync(locationId);

        if (location is null)
        {
            return;
        }

        _dbContext.Locations.Remove(location);
        await _dbContext.SaveChangesAsync();
    }
}