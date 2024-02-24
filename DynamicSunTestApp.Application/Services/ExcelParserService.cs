using System.Globalization;
using DynamicSunTestApp.Application.Extensions;
using DynamicSunTestApp.Application.Services.Interfaces;
using DynamicSunTestApp.Domain.Entities;
using Microsoft.AspNetCore.Http;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace DynamicSunTestApp.Application.Services;

public class ExcelParserService : IExcelParserService
{
    public List<WeatherArchiveRecordEntity> ParseWeatherArchiveRecordsAsync(IFormFile file, Guid weatherArchiveId)
    {
        IWorkbook workbook;

        using (var fileStream = file.OpenReadStream())
        {
            workbook = new XSSFWorkbook(fileStream);
        }

        var weatherArchiveRecordList = new List<WeatherArchiveRecordEntity>();
        
        for (var i = 0; i < workbook.NumberOfSheets; i++)
        {
            var sheet = workbook.GetSheetAt(i);
            var rowCount = sheet.LastRowNum;

            for (var j = 4; j <= rowCount; j++)
            {
                var row = sheet.GetRow(j);

                if (row == null) continue;

                var cultureInfo = new CultureInfo("ru-RU");

                var date = DateOnly.Parse(row.GetCell(0).ToCellValueString(), cultureInfo);
                var time = TimeOnly.Parse(row.GetCell(1).ToCellValueString(), cultureInfo);
                var temp = double.Parse(row.GetCell(2).ToCellValueString());
                var humidity = double.Parse(row.GetCell(3).ToCellValueString());
                var dewPoint = double.Parse(row.GetCell(4).ToCellValueString());
                var atmosphericPressure = int.Parse(row.GetCell(5).ToCellValueString());
                var windDirection = row.GetCell(6).ToCellValueString();
                var isThereWindSpeed = int.TryParse(row.GetCell(7).ToCellValueString(), out var windSpeed);
                var isThereCloudiness = int.TryParse(row.GetCell(8).ToCellValueString(), out var cloudiness);
                var isThereLowerBoundaryOfCloudiness = int.TryParse(row.GetCell(9).ToCellValueString(), out var lowerBoundaryOfCloudiness);
                var isThereHorizontalVisibility = int.TryParse(row.GetCell(10).ToCellValueString(), out var horizontalVisibility);
                var weatherEvents = row.GetCell(11).ToCellValueString();

                var weatherArchiveRecordEntity = new WeatherArchiveRecordEntity(
                    date,
                    time,
                    temp,
                    humidity,
                    dewPoint,
                    atmosphericPressure,
                    windDirection,
                    isThereWindSpeed ? windSpeed : null,
                    isThereCloudiness ? cloudiness : null,
                    isThereLowerBoundaryOfCloudiness ? lowerBoundaryOfCloudiness : null,
                    isThereHorizontalVisibility ? horizontalVisibility : null,
                    weatherEvents,
                    weatherArchiveId);

                weatherArchiveRecordList.Add(weatherArchiveRecordEntity);
            }
        }

        return weatherArchiveRecordList;
    }
}