using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaDotNet.Api.Contracts
{
    public interface ILoggerService
    {
        void LogInfo(string message);
        void LogError(Exception e);
        void LogDebug(string message);
        void LogWarn(string message);
    }
}
