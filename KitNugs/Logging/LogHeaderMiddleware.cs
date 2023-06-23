namespace KitNugs.Logging
{
    public class LogHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public LogHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var header = context.Request.Headers[ISessionIdAccessor.REQUEST_ID_HEADER];
            string sessionId;
            bool foundInHeader = false;

            if (header.Count > 0)
            {
                sessionId = header[0];
                foundInHeader = true;
            }
            else
            {
                sessionId = Guid.NewGuid().ToString();
            }

            context.Items[ISessionIdAccessor.REQUEST_ID_HEADER] = sessionId;

            var logger = context.RequestServices.GetRequiredService<ILogger<LogHeaderMiddleware>>();
            using (logger.BeginScope("{@CorrelationId}", sessionId))
            {
                logger.LogDebug("Request ID found in header: {foundInHeader}.", foundInHeader);
                await this._next(context);
            }
        }
    }
}
