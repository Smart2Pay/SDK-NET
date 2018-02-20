using System;
using S2p.RestClient.Sdk.Infrastructure.Extensions;

namespace S2p.RestClient.Sdk.Infrastructure.Logging
{
    public class LoggerAdapterFactory
    {
        private Func<string, ILoggerAdapter> _adapterFactory;
        private readonly object _lock = new object(); 

        internal Func<string, ILoggerAdapter> Get
        {
            get
            {
                lock (_lock)
                {
                    return _adapterFactory.ValueIfNull(() => loggerType => new NullLoggerAdapter());
                }
            }
        }

        public void RegisterAdapterFactory(Func<string, ILoggerAdapter> loggerFactory)
        {
            lock (_lock)
            {
                _adapterFactory = loggerFactory;
            }
        }
    }
}
