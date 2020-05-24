using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NinjaDotNet.Api.Contracts;
using NLog;

namespace NinjaDotNet.Api.Services
{
    public class LoggerService : ILoggerService
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public void LogInfo(string message)
        {
            Logger.Info(message);
        }

        public void LogError(Exception e)
        {
            Logger.Error(e);
        }

        public void LogDebug(string message)
        {
            Logger.Debug(message);
        }

        public void LogWarn(string message)
        {
            Logger.Warn(message);
        }
    }
}
