using DynamicSunTestApp.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace DynamicSunTestApp.Application.Services.Interfaces;

public interface IExcelParserService
{
    List<WeatherArchiveRecordEntity> ParseWeatherArchiveRecordsAsync(IFormFile file, Guid weatherArchiveId);
}