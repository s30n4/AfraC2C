using System;
using Microsoft.Extensions.Logging;

namespace AC2C.Application.Identity.Logger
{
    public static class DbLoggerFactoryExtensions
    {
        public static ILoggerFactory AddDbLogger(
                    this ILoggerFactory factory,
                    IServiceProvider serviceProvider,
                    LogLevel minLevel)
        {
            Func<string, LogLevel, bool> logFilter = (loggerName, logLevel) => logLevel >= minLevel;

            factory.AddProvider(new DbLoggerProvider(logFilter, serviceProvider));
            return factory;
        }
    }
}