namespace TAL.Developer.Test.Domain.Logging
{
    public interface ILogger
    {
        void Log(string message);
        void LogError(string message);
    }
}