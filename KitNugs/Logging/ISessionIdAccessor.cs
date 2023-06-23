namespace KitNugs.Logging
{
    public interface ISessionIdAccessor
    {
        public const string REQUEST_ID_HEADER = "X-Request-Id";
        string GetSessionId();
    }
}
