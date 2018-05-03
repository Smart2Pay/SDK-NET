using System;

namespace S2p.RestClient.Sdk.Infrastructure.Logging
{
    internal class NullLogger : ILogger
    {
        public void LogDebug(object message) { }
        public void LogDebug(object message, Exception exception) { }
        public void LogError(object message) { }
        public void LogError(object message, Exception exception) { }
        public void LogInfo(object message) { }
        public void LogInfo(object message, Exception exception) { }
        public void LogWarn(object message) { }
        public void LogWarn(object message, Exception exception) { }
    }
}
