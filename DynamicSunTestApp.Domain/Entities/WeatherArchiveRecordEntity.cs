
namespace DynamicSunTestApp.Domain.Entities;

public class WeatherArchiveRecordEntity
{
    public Guid Id { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly Time { get; set; }

    public double Temperature { get; set; }
    
    public double Humidity { get; set; }
    
    public double DewPoint { get; set; }
    
    public int AtmosphericPressure { get; set; }
    
    public string? WindDirection { get; set; }
    
    public int? WindSpeed { get; set; }
    
    public int? Cloudiness { get; set; }
    
    public int? LowerBoundaryOfCloudiness { get; set; }
    
    public int? HorizontalVisibility { get; set; }

    public string? WeatherEvents { get; set; }
    
    public Guid WeatherArchiveId { get; set; }

    public WeatherArchiveRecordEntity(
        DateOnly date, 
        TimeOnly time,
        double temperature,
        double humidity,
        double dewPoint,
        int atmosphericPressure,
        string? windDirection,
        int? windSpeed,
        int? cloudiness,
        int? lowerBoundaryOfCloudiness,
        int? horizontalVisibility,
        string? weatherEvents,
        Guid weatherArchiveId)
    {
        Id = Guid.NewGuid();
        Date = date;
        Time = time;
        Temperature = temperature;
        Humidity = humidity;
        DewPoint = dewPoint;
        AtmosphericPressure = atmosphericPressure;
        WindDirection = windDirection;
        WindSpeed = windSpeed;
        Cloudiness = cloudiness;
        LowerBoundaryOfCloudiness = lowerBoundaryOfCloudiness;
        HorizontalVisibility = horizontalVisibility;
        WeatherEvents = weatherEvents;
        WeatherArchiveId = weatherArchiveId;
    }
}