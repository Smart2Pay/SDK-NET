using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace S2p.RestClient.Sdk.Infrastructure.Logging
{
    public class LoggerProvider
    {
        private readonly ILogger _nullLogger = new NullLogger();
        private readonly ConcurrentDictionary<string, ILogger> _loggerCache = new ConcurrentDictionary<string, ILogger>();

        internal Func<string, ILogger> LoggerFactory { get; private set; }
        internal IReadOnlyDictionary<string, ILogger> LoggerCache => _loggerCache;

        public ILogger GetLogger<T>()
        {
            return GetLogger(typeof(T).FullName);
        }

        public ILogger GetLogger(string loggerName)
        {
            return string.IsNullOrWhiteSpace(loggerName) || LoggerFactory == null
                ? _nullLogger
                : _loggerCache.GetOrAdd(loggerName, LoggerFactory(loggerName));
        }

        public void RegisterLoggerFactory(Func<string, ILogger> loggerFactory)
        {
            LoggerFactory = loggerFactory;
            _loggerCache.Clear();
        }
    }
}
