using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Middlewares;
public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception exception)
        {
            await SendExceptionToTelegramGroupAsync(exception);
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(new { error = exception.Message });
        }
    }
    private async Task SendExceptionToTelegramGroupAsync(Exception exception)
    {
        using var client = new HttpClient();
        var result = await client.GetAsync($"https://api.telegram.org/bot5607955006:AAGHBStoV2pffc9lmuvlpQ_a7UgrU8JYcmM/sendmessage?chat_id=-827851635&text={exception}");
        _logger.LogInformation($"Exception was send to telegram. Status: {result.StatusCode.ToString()}");
    }
}