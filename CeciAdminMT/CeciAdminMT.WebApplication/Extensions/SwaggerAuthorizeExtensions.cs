using CeciAdminMT.WebApplication.Middlewares;
using Microsoft.AspNetCore.Builder;
using System.Diagnostics.CodeAnalysis;

namespace CeciAdminMT.WebApplication.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class SwaggerAuthorizeExtensions
    {
        public static IApplicationBuilder UseSwaggerAuthorized(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SwaggerBasicAuthMiddleware>();
        }
    }
}
