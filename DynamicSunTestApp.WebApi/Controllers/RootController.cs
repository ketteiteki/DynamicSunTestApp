using DynamicSunTestApp.Domain.Constants;
using Microsoft.AspNetCore.Mvc;

namespace DynamicSunTestApp.WebApi.Controllers;

public class RootController : Controller
{
    [HttpGet("/")]
    public IActionResult RedirectToTheAngularSpa()
    {
        return Redirect($@"~{SpaRouting.App}");
    }
}