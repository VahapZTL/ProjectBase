using Core.CrossCuttingConcerns.Logging.NLogLogger.Abstract;
using Microsoft.Extensions.Logging;
using NLog;
using System.Collections.Concurrent;
using System.Reflection;

namespace Core.CrossCuttingConcerns.Logging.NLogLogger.Concrete
{
    public class Logger : AbstractLogger, ILoggerProvider, IDisposable
    {
        private readonly ConcurrentDictionary<string, Logger> _loggers = new ConcurrentDictionary<string, Logger>();

        private readonly NLog.Logger log = LogManager.CreateNullLogger();

        public Logger()
        {
            log = LogManager.GetCurrentClassLogger();
        }

        public Logger(Type type)
        {
            log = LogManager.GetLogger(type.FullName);
        }

        public override IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public Microsoft.Extensions.Logging.ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, CreateLoggerImplementation(categoryName));
        }

        public void Dispose()
        {
            _loggers.Clear();
        }

        private Logger CreateLoggerImplementation(string name)
        {
            WriteLog(Microsoft.Extensions.Logging.LogLevel.Information, null, "CREATING LOG  FOR CATEGORY :" + name);
            return new Logger();
        }

        public override bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel)
        {
            switch (logLevel)
            {
                case Microsoft.Extensions.Logging.LogLevel.Critical:
                    return log.IsFatalEnabled;
                case Microsoft.Extensions.Logging.LogLevel.Trace:
                case Microsoft.Extensions.Logging.LogLevel.Debug:
                    return log.IsDebugEnabled;
                case Microsoft.Extensions.Logging.LogLevel.Error:
                    return log.IsErrorEnabled;
                case Microsoft.Extensions.Logging.LogLevel.Information:
                    return log.IsInfoEnabled;
                case Microsoft.Extensions.Logging.LogLevel.Warning:
                    return log.IsWarnEnabled;
                default:
                    throw new ArgumentOutOfRangeException("logLevel");
            }
        }

        public override void Log<TState>(Microsoft.Extensions.Logging.LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (formatter == null)
            {
                throw new ArgumentNullException("formatter");
            }

            string text = null;
            if (formatter != null)
            {
                text = formatter(state, exception);
            }

            if (!string.IsNullOrEmpty(text) || exception != null)
            {
                switch (logLevel)
                {
                    case Microsoft.Extensions.Logging.LogLevel.Critical:
                        WriteLog(Microsoft.Extensions.Logging.LogLevel.Critical, (exception != null) ? exception : null, text);
                        break;
                    case Microsoft.Extensions.Logging.LogLevel.Trace:
                    case Microsoft.Extensions.Logging.LogLevel.Debug:
                        WriteLog(Microsoft.Extensions.Logging.LogLevel.Debug, (exception != null) ? exception : null, text);
                        break;
                    case Microsoft.Extensions.Logging.LogLevel.Error:
                        WriteLog(Microsoft.Extensions.Logging.LogLevel.Error, (exception != null) ? exception : null, text);
                        break;
                    case Microsoft.Extensions.Logging.LogLevel.Information:
                        WriteLog(Microsoft.Extensions.Logging.LogLevel.Information, (exception != null) ? exception : null, text);
                        break;
                    case Microsoft.Extensions.Logging.LogLevel.Warning:
                        WriteLog(Microsoft.Extensions.Logging.LogLevel.Warning, (exception != null) ? exception : null, text);
                        break;
                    default:
                        WriteLog(Microsoft.Extensions.Logging.LogLevel.Warning, (exception != null) ? exception : null, text);
                        break;
                }
            }
        }

        public override void WriteLog(Microsoft.Extensions.Logging.LogLevel logType, Exception exception, string message)
        {
            switch (logType)
            {
                case Microsoft.Extensions.Logging.LogLevel.Information:
                    if (log.IsInfoEnabled)
                    {
                        log.Info(exception, message);
                    }

                    break;
                case Microsoft.Extensions.Logging.LogLevel.Debug:
                    if (log.IsDebugEnabled)
                    {
                        log.Debug(exception, message);
                    }

                    break;
                case Microsoft.Extensions.Logging.LogLevel.Warning:
                    if (log.IsWarnEnabled)
                    {
                        log.Warn(exception, message);
                    }

                    break;
                case Microsoft.Extensions.Logging.LogLevel.Error:
                    if (log.IsErrorEnabled)
                    {
                        log.Error(exception, message, null);
                    }

                    break;
                case Microsoft.Extensions.Logging.LogLevel.Critical:
                    if (log.IsFatalEnabled)
                    {
                        log.Fatal(exception, message);
                    }

                    break;
                default:
                    if (log.IsInfoEnabled)
                    {
                        log.Info(exception, message);
                    }

                    break;
            }
        }
    }
}
