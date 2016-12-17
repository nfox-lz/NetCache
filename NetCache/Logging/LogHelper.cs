namespace Compete.NetCache.Logging
{
    public static class LogHelper
    {
        public static ILogger Logger { get; private set; }

        static LogHelper()
        {
            Logger = new ElLogger();
        }
    }
}
