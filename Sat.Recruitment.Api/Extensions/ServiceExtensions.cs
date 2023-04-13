using Microsoft.AspNetCore.Builder;
using Sat.Recruitment.Api.Middlewares;

namespace Sat.Recruitment.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IApplicationBuilder TraceMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TraccingMiddleware>();
        }

    }
}
