using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Middlewares;
public class LanguageMiddleware
{
    private readonly RequestDelegate _next;

    public LanguageMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public Task Invoke(HttpContext httpContext)
    {
        if (!httpContext.Request.Headers.ContainsKey("language"))
        {
            throw new Exception("Language header missed!");
        }

        RequestCulture.RequestLanguage = httpContext.Request.Headers["language"];

        return _next(httpContext);
    }
}