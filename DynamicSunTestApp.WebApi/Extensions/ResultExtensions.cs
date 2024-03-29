using DynamicSunTestApp.Domain.Responses;
using Microsoft.AspNetCore.Mvc;

namespace DynamicSunTestApp.WebApi.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToActionResult<T>(this Result<T> result)
    {
        return result.Error != null ? 
            new ObjectResult(new { result.Error.Message }) { StatusCode = StatusCodes.Status400BadRequest } 
            : new ObjectResult(new { result.Response }) { StatusCode = StatusCodes.Status200OK};
    }
}