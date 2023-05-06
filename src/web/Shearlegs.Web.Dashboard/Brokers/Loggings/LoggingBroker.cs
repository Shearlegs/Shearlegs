using Microsoft.Extensions.Logging;
using System;

namespace Shearlegs.Web.Dashboard.Brokers.Loggings
{
    public class LoggingBroker : ILoggingBroker
    {
        private readonly ILogger<LoggingBroker> logger;

        public LoggingBroker(ILogger<LoggingBroker> logger)
        {
            this.logger = logger;
        }

        public void LogInformation(string message)
        {
            logger.LogInformation(message);
        }

        public void LogDebug(string message)
        {
            logger.LogDebug(message);
        }

        public void LogException(Exception exception)
        {
            logger.LogError(exception, exception.Message);
        }
    }
}
