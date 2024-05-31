using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Weather.Forecast.API.Requests;
using Weather.Forecast.API.Responses;
using Weather.Forecast.API.Services.Abstractions;
using Weather.Forecast.API.Services.Models;

namespace Weather.Forecast.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherForecastService _weatherForecastService;
    private readonly IMapper _mapper;

    public WeatherForecastController(IWeatherForecastService weatherForecastService, IMapper mapper)
    {
        _weatherForecastService = weatherForecastService;
        _mapper = mapper;
    }
    
    [HttpGet("{locationId}")]
    public async Task<IActionResult> GetWeatherForecast(int locationId)
    {
        var location = await _weatherForecastService.GetForecast(locationId);

        if (location is null)
        {
            return NotFound();
        }

        var response = _mapper.Map<WeatherLocationResponse>(location);
        
        return Ok(response);
    }
}