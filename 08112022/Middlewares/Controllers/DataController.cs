using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Middlewares.Filters;

namespace Middlewares.Controllers;
[Route("api/[controller]")]
[ApiController]
[LanguageFilter]
public class DataController : ControllerBase
{
    private readonly List<string> _uzData = new() { "Malumot1", "Malumot2" };
    private readonly List<string> _enData = new() { "data1", "data2" };

    [HttpGet]
    public IActionResult GetDataList()
    {
        return RequestCulture.RequestLanguage switch
        {
            "en" => Ok(_enData),
            "uz" => Ok(_uzData),
            _ => throw new InvalidOperationException()
        };
    }
}
