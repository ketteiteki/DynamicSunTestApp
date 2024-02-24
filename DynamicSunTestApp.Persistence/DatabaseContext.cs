using DynamicSunTestApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DynamicSunTestApp.Persistence;

public class DatabaseContext : DbContext
{
    public DbSet<WeatherArchiveEntity> WeatherArchiveEntities { get; set; }
    public DbSet<WeatherArchiveRecordEntity> WeatherArchiveRecordEntities { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> dbContextOptions) : base(dbContextOptions)
    {
    }
	
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<WeatherArchiveEntity>()
            .HasMany(x => x.WeatherArchiveRecordEntities)
            .WithOne()
            .HasForeignKey(x => x.WeatherArchiveId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}