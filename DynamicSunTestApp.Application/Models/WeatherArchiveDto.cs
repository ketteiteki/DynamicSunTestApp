namespace DynamicSunTestApp.Application.Models;

public record WeatherArchiveDto(
    Guid Id,
    string Name,
    int RecordCount);