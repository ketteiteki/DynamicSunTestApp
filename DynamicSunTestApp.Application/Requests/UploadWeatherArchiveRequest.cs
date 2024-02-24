using Microsoft.AspNetCore.Http;

namespace DynamicSunTestApp.Application.Requests;

public record UploadWeatherArchiveRequest(IFormFile[] Files);