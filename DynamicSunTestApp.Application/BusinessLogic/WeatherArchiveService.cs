using DynamicSunTestApp.Application.Models;
using DynamicSunTestApp.Application.Services.Interfaces;
using DynamicSunTestApp.Domain.Constants;
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
    public async Task<Result<List<WeatherArchiveDto>>> GetWeatherArchiveListAsync(int offset, int limit)
    {
        var records = await context.WeatherArchiveEntities
            .Skip(offset)
            .Take(limit)
            .Select(x => new WeatherArchiveDto(x.Id, x.Name, x.WeatherArchiveRecordEntities.Count))
            .ToListAsync();

        return new Result<List<WeatherArchiveDto>>(records);
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
    
    public async Task<Result<List<WeatherArchiveDto>>> UploadWeatherArchiveAsync(IFormFile[] files)
    {
        if (files.Any(file => file.ContentType != ContentTypeConstants.Xlsx))
        {
            return new Result<List<WeatherArchiveDto>>(new Error(ErrorConstants.FilesAreNotExcel));
        }
        
        var weatherArchiveDtoList = new List<WeatherArchiveDto>();
        
        try
        {
            foreach (var file in files)
            {
                var weatherArchiveEntity = new WeatherArchiveEntity(file.FileName);

                var weatherArchiveRecordList = excelParserService.ParseWeatherArchiveRecordsAsync(file, weatherArchiveEntity.Id);
                
                context.WeatherArchiveEntities.Add(weatherArchiveEntity);
                context.WeatherArchiveRecordEntities.AddRange(weatherArchiveRecordList);

                var weatherArchiveDto = new WeatherArchiveDto(
                    weatherArchiveEntity.Id, 
                    weatherArchiveEntity.Name,
                    weatherArchiveRecordList.Count);
                
                weatherArchiveDtoList.Add(weatherArchiveDto);
            }
        }
        catch (Exception)
        {
            return new Result<List<WeatherArchiveDto>>(new Error(ErrorConstants.FilesAreInvalid));
        }

        await context.SaveChangesAsync();
        
        return new Result<List<WeatherArchiveDto>>(weatherArchiveDtoList);
    }
}