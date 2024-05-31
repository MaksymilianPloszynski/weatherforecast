using System.Reflection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Weather.Forecast.API.Middlewares;
using Weather.Forecast.API.Services;
using Weather.Forecast.API.Services.Abstractions;
using Weather.Forecast.Infrastructure.Clients;
using Weather.Forecast.Infrastructure.Clients.Abstractions;
using Weather.Forecast.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddHttpClient<IWeatherClient, WeatherClient>(client =>
{
    client.BaseAddress = new Uri("https://api.open-meteo.com/v1/forecast");
});

builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();

builder.Services.AddDbContext<WeatherForecastDbContext>(options =>
    options.UseInMemoryDatabase(builder.Configuration.GetConnectionString("WeatherForecastDatabase")!));

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblies(new [] { Assembly.GetEntryAssembly() });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionsHandlingMiddleware>();

app.Run();