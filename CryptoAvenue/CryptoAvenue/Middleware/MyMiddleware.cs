using CryptoAvenue.Services;

namespace CryptoAvenue.Middleware
{
    public class MyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MyMiddleware> _logger;
        private readonly ISingletonService _singleton;
        private readonly ITransientService _transient;

        public MyMiddleware(RequestDelegate next, ILogger<MyMiddleware> logger, ISingletonService singleton, ITransientService transient)
        {
            _next = next;
            _logger = logger;
            _singleton = singleton;
            _transient = transient;
            _logger.LogInformation(_singleton.Guid.ToString());
            _logger.LogInformation(_transient.Guid.ToString());
        }

        public Task Invoke(HttpContext httpContext, IScopedService scoped)
        {
            _logger.LogInformation(_singleton.Guid.ToString());
            _logger.LogInformation(_transient.Guid.ToString());
            _logger.LogInformation(scoped.Guid.ToString());
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MyMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyMiddleware>();
        }
    }
}
