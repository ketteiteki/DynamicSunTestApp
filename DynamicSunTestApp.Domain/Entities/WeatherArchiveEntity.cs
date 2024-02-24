namespace DynamicSunTestApp.Domain.Entities;

public class WeatherArchiveEntity
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }

    public List<WeatherArchiveRecordEntity> WeatherArchiveRecordEntities { get; set; } = new();

    public WeatherArchiveEntity(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
}