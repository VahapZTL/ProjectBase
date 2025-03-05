﻿namespace Core.CrossCuttingConcerns.Logging.NLogLogger.Abstract
{
    public interface ILogger : Microsoft.Extensions.Logging.ILogger
    {
        void WriteLog(Microsoft.Extensions.Logging.LogLevel logType, Exception exception, string message);

        void Info(string message);

        void Info(Exception exception, string message);

        void Debug(string message);

        void Debug(Exception exception, string message);

        void Warning(string message);

        void Warning(Exception exception, string message);

        void Error(string message);

        void Error(Exception exception, string message);

        void Fatal(string message);

        void Fatal(Exception exception, string message);
    }
}
