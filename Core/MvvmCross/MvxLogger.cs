using Microsoft.Extensions.Logging;
using MvvmCross.Binding;
using System;
namespace Core.Logging
{
    // https://blog.rsuter.com/logging-with-ilogger-recommendations-and-best-practices/

    public class MvxLogger : ILogger
    {
        public static LogLevel LogLevel { get; set; }

        public IDisposable BeginScope<TState>(TState state) => new MvxLoggerScope();

        public bool IsEnabled(LogLevel logLevel) => logLevel >= LogLevel;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (LogLevel <= LogLevel)
                return;

            if (logLevel == LogLevel.Error || logLevel == LogLevel.Critical)
                Dlog.Error(state);
            else if (logLevel == LogLevel.Warning)
                Dlog.Warning(state);
            else
                Dlog.Info(state);

            if (exception != null)
                Dlog.Error(exception.Message);
        }
    }

    public class MvxLoggerScope : IDisposable
    {
        public void Dispose() => GC.SuppressFinalize(this);
    }

    public class MvxLoggerProvider : ILoggerProvider
    {
        private bool disposedValue;

        public ILogger CreateLogger(string categoryName) => new MvxLogger();

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects)
                }

                // free unmanaged resources (unmanaged objects) and override finalizer
                // set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

    public class MvxLoggerFactory : ILoggerFactory
    {
        private bool disposedValue;

        public void AddProvider(ILoggerProvider provider) { }
        public ILogger CreateLogger(string categoryName) => new MvxLogger();

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects)
                }

                // free unmanaged resources (unmanaged objects) and override finalizer
                // set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
