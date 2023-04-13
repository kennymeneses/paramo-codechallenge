using BusinessLogic.Traccing;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Middlewares
{
    public class TraccingMiddleware
    {
        private readonly RequestDelegate _next;
        private const string _correlationIdHeader = "CorrelationId";
        
        public TraccingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            CorrelationIdGenerator.Instance.GenerateCorrelationId();

            var correlationId = CorrelationIdGenerator.Instance.GetCorrelationId();
            AddCorrelationIdToResponse(context, correlationId);

            await _next(context);
        }

        private static void AddCorrelationIdToResponse(HttpContext context, string correlationId)
        {
            context.Response.OnStarting(() => {
                context.Response.Headers.Add(_correlationIdHeader, new[] { correlationId });
                return Task.CompletedTask;
            });
        }
    }
}
