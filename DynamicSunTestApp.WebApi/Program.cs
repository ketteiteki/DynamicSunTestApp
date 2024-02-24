using DynamicSunTestApp.Application.BusinessLogic;
using DynamicSunTestApp.Application.Services;
using DynamicSunTestApp.Application.Services.Interfaces;
using DynamicSunTestApp.Domain.Constants;
using DynamicSunTestApp.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration[AppSettingConstants.DatabaseConnectionString];

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddScoped<WeatherArchiveService>();
builder.Services.AddScoped<IExcelParserService, ExcelParserService>();

builder.Services.AddDbContext<DatabaseContext>(option =>
{
    option.UseNpgsql(connectionString);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();