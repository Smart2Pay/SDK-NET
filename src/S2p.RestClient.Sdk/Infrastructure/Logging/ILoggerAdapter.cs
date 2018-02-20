using System;

namespace S2p.RestClient.Sdk.Infrastructure.Logging
{
    public interface ILoggerAdapter
    {
        void LogInfo(object message);
        void LogInfo(object message, Exception exception);
        void LogDebug(object message);
        void LogDebug(object message, Exception exception);
        void LogWarn(object message);
        void LogWarn(object message, Exception exception);
        void LogError(object message);
        void LogError(object message, Exception exception);
    }
}
