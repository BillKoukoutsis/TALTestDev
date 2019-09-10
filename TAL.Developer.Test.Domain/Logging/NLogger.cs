using NLog;

namespace TAL.Developer.Test.Domain.Logging
{
    public class NLogger : ILogger
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public void Log(string message)
        {
            logger.Log(LogLevel.Info, message);
        }

        public void LogError(string message)
        {
            logger.Log(LogLevel.Error, message);
        }
    }
}