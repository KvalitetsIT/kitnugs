namespace KitNugs.Logging
{

    public class DefaultSessionIdAccessor : ISessionIdAccessor
    {
        private readonly ILogger<DefaultSessionIdAccessor> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DefaultSessionIdAccessor(ILogger<DefaultSessionIdAccessor> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetSessionId()
        {
            try
            {
                var context = this._httpContextAccessor.HttpContext;
                var result = context?.Items[ISessionIdAccessor.REQUEST_ID_HEADER] as string;

                return result;
            }
            catch (Exception exception)
            {
                this._logger.LogWarning(exception, "Unable to get original session id header");
            }

            return string.Empty;

        }
    }
}
