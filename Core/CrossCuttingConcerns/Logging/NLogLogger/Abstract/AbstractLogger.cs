using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging.NLogLogger.Abstract
{
    public abstract class AbstractLogger : ILogger, Microsoft.Extensions.Logging.ILogger
    {
        public abstract void WriteLog(LogLevel logType, Exception exception, string message);

        void ILogger.WriteLog(LogLevel logType, Exception exception, string message)
        {
            WriteLog(logType, exception, message);
        }

        public void Debug(string message)
        {
            WriteLog(LogLevel.Debug, null, message);
        }

        public void Debug(Exception exception, string message)
        {
            WriteLog(LogLevel.Debug, exception, message);
        }

        public void Error(string message)
        {
            WriteLog(LogLevel.Error, null, message);
        }

        public void Error(Exception exception, string message)
        {
            WriteLog(LogLevel.Error, exception, message);
        }

        public void Fatal(string message)
        {
            WriteLog(LogLevel.Critical, null, message);
        }

        public void Fatal(Exception exception, string message)
        {
            WriteLog(LogLevel.Critical, exception, message);
        }

        public void Info(string message)
        {
            WriteLog(LogLevel.Information, null, message);
        }

        public void Info(Exception exception, string message)
        {
            WriteLog(LogLevel.Information, exception, message);
        }

        public void Warning(string message)
        {
            WriteLog(LogLevel.Warning, null, message);
        }

        public void Warning(Exception exception, string message)
        {
            WriteLog(LogLevel.Warning, exception, message);
        }

        public abstract void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter);

        public abstract bool IsEnabled(LogLevel logLevel);

        public abstract IDisposable BeginScope<TState>(TState state);
    }
}
