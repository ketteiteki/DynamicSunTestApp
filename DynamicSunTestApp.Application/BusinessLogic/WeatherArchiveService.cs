using DynamicSunTestApp.Application.Services.Interfaces;
using DynamicSunTestApp.Domain.Entities;
using DynamicSunTestApp.Domain.Responses;
using DynamicSunTestApp.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DynamicSunTestApp.Application.BusinessLogic;

public class WeatherArchiveService(
    DatabaseContext context,
    IExcelParserService excelParserService)
{
    public async Task<Result<List<WeatherArchiveEntity>>> GetWeatherArchiveListAsync(int offset, int limit)
    {
        var records = await context.WeatherArchiveEntities
            .Skip(offset)
            .Take(limit)
            .ToListAsync();

        return new Result<List<WeatherArchiveEntity>>(records);
    }
    
    public async Task<Result<List<WeatherArchiveRecordEntity>>> GetWeatherArchiveRecordListAsync(Guid weatherArchiveId, int offset, int limit)
    {
        var records = await context.WeatherArchiveRecordEntities
            .Where(x => x.WeatherArchiveId == weatherArchiveId)
            .OrderBy(x => x.Date)
            .ThenBy(x => x.Time)
            .Skip(offset)
            .Take(limit)
            .ToListAsync();

        return new Result<List<WeatherArchiveRecordEntity>>(records);
    }
    
    public async Task<Result<List<WeatherArchiveEntity>>> UploadWeatherArchiveAsync(IFormFile[] files)
    {
        var weatherArchiveEntityList = new List<WeatherArchiveEntity>();
        
        try
        {
            foreach (var file in files)
            {
                var weatherArchiveEntity = new WeatherArchiveEntity(file.FileName);

                var weatherArchiveRecordList = excelParserService.ParseWeatherArchiveRecordsAsync(file, weatherArchiveEntity.Id);
                
                context.WeatherArchiveEntities.Add(weatherArchiveEntity);
                context.WeatherArchiveRecordEntities.AddRange(weatherArchiveRecordList);
                await context.SaveChangesAsync();

                weatherArchiveEntityList.Add(weatherArchiveEntity);
            }
        }
        catch (Exception)
        {
            return new Result<List<WeatherArchiveEntity>>(new Error("The files are invalid"));
        }

        return new Result<List<WeatherArchiveEntity>>(weatherArchiveEntityList);
    }
}