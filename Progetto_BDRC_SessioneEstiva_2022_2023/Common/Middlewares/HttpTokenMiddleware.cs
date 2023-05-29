using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Progetto_BDRC_SessioneEstiva_2022_2023.Api.Common.Middlewares;

public class HttpTokenMiddleware
{
    private readonly RequestDelegate _next;
    //private readonly ILogger _logger;

    public HttpTokenMiddleware(RequestDelegate next
        // ILoggerFactory logFactory
        )
    {
        _next = next;

        //_logger = logFactory.CreateLogger("HttpTokenMiddleware");
    }

    public async Task Invoke(HttpContext httpContext)
    {
        //_logger.LogInformation("MyMiddleware executing..");
        HttpContextAccessor httpContextAccessor = new HttpContextAccessor();
        var httpC = httpContextAccessor.HttpContext ?? null;
        if(httpC is not null)
        {
            var tt = await httpC.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
        }

        await _next(httpContext); // calling next middleware

    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class HttpTokenMiddlewareExtensions
{
    public static IApplicationBuilder UseHttpTokenMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<HttpTokenMiddleware>();
    }
}
