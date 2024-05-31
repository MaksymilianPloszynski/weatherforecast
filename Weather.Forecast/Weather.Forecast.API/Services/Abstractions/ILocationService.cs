using Weather.Forecast.API.Services.Models;

namespace Weather.Forecast.API.Services.Abstractions;

public interface ILocationService
{
    Task<PagedResult<LocationInfoModel>> GetAllLocations(int pageNumber, int pageSize);
    Task<LocationInfoModel> AddLocation(double latitude, double longitude);
    Task DeleteLocation(int locationId);
}