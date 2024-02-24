using DynamicSunTestApp.Application.Services;
using DynamicSunTestApp.Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Xunit;

namespace DynamicSunTestApp.UnitTests.Services;

public class ExcelParserServiceTests
{
    [Fact]
    public void ParseWeatherArchiveRecordsAsync_Success()
    {
        IWorkbook workbook = new XSSFWorkbook();
        var random = new Random();
        var preparedRecords = new List<WeatherArchiveRecordEntity>();
        for (var j = 0; j < 4; j++)
        {
            ISheet sheet = workbook.CreateSheet($"Sheet{j}");
            for (var i = 4; i < 30; i++)
            {
                IRow row = sheet.CreateRow(i);
                var weatherArchiveRecordEntity = new WeatherArchiveRecordEntity(
                    new DateOnly(2024, random.Next(1, 10), random.Next(1, 25)),
                    new TimeOnly(random.Next(1, 20), random.Next(1, 50)),
                    GenerateDouble(0, 100),
                    GenerateDouble(0, 100),
                    GenerateDouble(0, 100),
                    random.Next(1, 100),
                    Guid.NewGuid().ToString(),
                    random.Next(1, 10),
                    random.Next(1, 100),
                    random.Next(1, 100),
                    random.Next(1, 100),
                    Guid.NewGuid().ToString(),
                    Guid.NewGuid()
                );
                row.CreateCell(0).SetCellValue(weatherArchiveRecordEntity.Date.ToString("dd.MM.yyyy"));
                row.CreateCell(1).SetCellValue(weatherArchiveRecordEntity.Time.ToString("HH:mm"));
                row.CreateCell(2).SetCellValue(weatherArchiveRecordEntity.Temperature);
                row.CreateCell(3).SetCellValue(weatherArchiveRecordEntity.Humidity);
                row.CreateCell(4).SetCellValue(weatherArchiveRecordEntity.DewPoint);
                row.CreateCell(5).SetCellValue(weatherArchiveRecordEntity.AtmosphericPressure);
                row.CreateCell(6).SetCellValue(weatherArchiveRecordEntity.WindDirection);
                row.CreateCell(7).SetCellValue(weatherArchiveRecordEntity.WindSpeed ?? 0);
                row.CreateCell(8).SetCellValue(weatherArchiveRecordEntity.Cloudiness ?? 0);
                row.CreateCell(9).SetCellValue(weatherArchiveRecordEntity.LowerBoundaryOfCloudiness ?? 0);
                row.CreateCell(10).SetCellValue(weatherArchiveRecordEntity.HorizontalVisibility ?? 0);
                row.CreateCell(11).SetCellValue(weatherArchiveRecordEntity.WeatherEvents);
                preparedRecords.Add(weatherArchiveRecordEntity);
            }
        }
        using var memoryStream = new MemoryStream();
        workbook.Write(memoryStream, true);
        var name = Guid.NewGuid().ToString();
        var formFile = new FormFile(memoryStream,  0, memoryStream.Length, name, $"{name}.xlsx")
        {
            Headers = new HeaderDictionary(),
            ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
        };

        var excelParserService = new ExcelParserService();
        var records = excelParserService.ParseWeatherArchiveRecordsAsync(formFile, Guid.NewGuid());

        for (var i = 0; i < preparedRecords.Count; i++)
        {
            preparedRecords[i].Date.Should().Be(records[i].Date);
            preparedRecords[i].Time.Should().Be(records[i].Time);
            preparedRecords[i].Temperature.Should().Be(records[i].Temperature);
            preparedRecords[i].Humidity.Should().Be(records[i].Humidity);
            preparedRecords[i].DewPoint.Should().Be(records[i].DewPoint);
            preparedRecords[i].AtmosphericPressure.Should().Be(records[i].AtmosphericPressure);
            preparedRecords[i].WindDirection.Should().Be(records[i].WindDirection);
            preparedRecords[i].WindSpeed.Should().Be(records[i].WindSpeed);
            preparedRecords[i].Cloudiness.Should().Be(records[i].Cloudiness);
            preparedRecords[i].LowerBoundaryOfCloudiness.Should().Be(records[i].LowerBoundaryOfCloudiness);
            preparedRecords[i].HorizontalVisibility.Should().Be(records[i].HorizontalVisibility);
            preparedRecords[i].WeatherEvents.Should().Be(records[i].WeatherEvents);
        }
    }

    private double GenerateDouble(int min, int max)
    {
        var random = new Random();
        return random.NextDouble() * (max - min) + min;
    }
}