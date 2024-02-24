using DynamicSunTestApp.Application.BusinessLogic;
using DynamicSunTestApp.Application.Requests;
using DynamicSunTestApp.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DynamicSunTestApp.WebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class WeatherArchiveController(WeatherArchiveService weatherArchiveService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetWeatherArchiveList([FromQuery] int offset = 0, [FromQuery] int limit = 30)
    {
        var result = await weatherArchiveService.GetWeatherArchiveListAsync(offset, limit);
        
        return result.ToActionResult();
    }
    
    [HttpGet("records/{weatherArchiveId:guid}")]
    public async Task<IActionResult> GetWeatherArchiveRecordList(Guid weatherArchiveId, [FromQuery] int offset = 0, [FromQuery] int limit = 30)
    {
        var result = await weatherArchiveService.GetWeatherArchiveRecordListAsync(weatherArchiveId, offset, limit);
        
        return result.ToActionResult();
    }
    
    [HttpPost]
    public async Task<IActionResult> UploadWeatherArchive([FromForm] UploadWeatherArchiveRequest request)
    {
        var result = await weatherArchiveService.UploadWeatherArchiveAsync(request.Files);
        
        return result.ToActionResult();
    }
}