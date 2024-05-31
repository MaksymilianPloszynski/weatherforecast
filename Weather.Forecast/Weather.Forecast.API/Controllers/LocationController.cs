using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Weather.Forecast.API.Requests;
using Weather.Forecast.API.Responses;
using Weather.Forecast.API.Services.Abstractions;
using Weather.Forecast.API.Services.Models;

namespace Weather.Forecast.API.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationController : ControllerBase
{
    private readonly ILocationService _locationService;
    private readonly IMapper _mapper;
    private readonly IValidator<GetAllLocationsRequest> _validator;

    public LocationController(ILocationService locationService, IMapper mapper, IValidator<GetAllLocationsRequest> validator)
    {
        _locationService = locationService;
        _mapper = mapper;
        _validator = validator;
    }
    
    [HttpGet("")]
    public async Task<IActionResult> GetAllLocation([FromQuery] GetAllLocationsRequest request)
    {
        var result = await _validator.ValidateAsync(request);//can be moved to middleware layer to handle model state globally
        
        if (!result.IsValid)
        {
            return BadRequest(result.Errors);
        }

        var pagedResult = await _locationService.GetAllLocations(request.PageNumber!.Value, request.PageSize!.Value);
        
        var response = new PagedResult<LocationInfoResponse>(
            _mapper.Map<List<LocationInfoResponse>>(pagedResult.Data), pagedResult.Pagination);
        
        return Ok(response);
    }
    
    [HttpPost("")]
    public async Task<IActionResult> AddLocation([FromBody] LocationRequest request)
    {
        var location = await _locationService.AddLocation(request.Latitude, request.Longitude);
        
        var response = _mapper.Map<LocationInfoResponse>(location);
        
        return Ok(response);
    }
    
    [HttpDelete("locationdId")]
    public async Task<IActionResult> DeleteLocation(int locationId)
    {
        await _locationService.DeleteLocation(locationId);
        return NoContent();
    }
}