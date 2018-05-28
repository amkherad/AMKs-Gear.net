using System;
using System.Collections.Generic;
using AMKsGear.Architecture.Trace;

namespace AMKsGear.Core.Trace
{
    public enum PrivateLoggerLoggingHost
    {
        DefaultLogger,
        LocalLogger,
        BothLoggers,
        LocalFirst,
        DisableLogging
    }
    public class LocalLogger : ILoggerEngine
    {
        private static ILoggerEngine _localEngine;
        private static PrivateLoggerLoggingHost _loggingHost = PrivateLoggerLoggingHost.DefaultLogger;

        private static Action<Exception, string, ILoggingContext> _loggerExceptionWriteMethod = _defaultLoggerEngineExceptionWrite;
        private static Action<string, string, ILoggingContext> _loggerStringWriteMethod = _defaultLoggerEngineStringWrite;
        private static Action<int> _loggerFeedMethod = _defaultLoggerEngineFeed;
        
        public void SetLocalLoggerEngine(ILoggerEngine logger) => _localEngine = logger;

        public static PrivateLoggerLoggingHost LoggingHost
        {
            get { return _loggingHost; }
            set
            {
                switch (value)
                {
                    case PrivateLoggerLoggingHost.DefaultLogger:
                        _loggerExceptionWriteMethod = _defaultLoggerEngineExceptionWrite;
                        _loggerStringWriteMethod = _defaultLoggerEngineStringWrite;
                        _loggerFeedMethod = _defaultLoggerEngineFeed;
                        break;
                    case PrivateLoggerLoggingHost.LocalLogger:
                        _loggerExceptionWriteMethod = _localLoggerEngineExceptionWrite;
                        _loggerStringWriteMethod = _localLoggerEngineStringWrite;
                        _loggerFeedMethod = _localLoggerEngineFeed;
                        break;
                    case PrivateLoggerLoggingHost.DisableLogging:
                        _loggerExceptionWriteMethod = _nullLoggerEngineExceptionWrite;
                        _loggerStringWriteMethod = _nullLoggerEngineStringWrite;
                        _loggerFeedMethod = _nullLoggerEngineFeed;
                        break;
                    case PrivateLoggerLoggingHost.BothLoggers:
                        _loggerExceptionWriteMethod = _bothLoggerEngineExceptionWrite;
                        _loggerStringWriteMethod = _bothLoggerEngineStringWrite;
                        _loggerFeedMethod = _bothLoggerEngineFeed;
                        break;
                    case PrivateLoggerLoggingHost.LocalFirst:
                        _loggerExceptionWriteMethod = _localFirstLoggerEngineExceptionWrite;
                        _loggerStringWriteMethod = _localFirstLoggerEngineStringWrite;
                        _loggerFeedMethod = _localFirstLoggerEngineFeed;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(value), value, null);
                }
                _loggingHost = value;
            }
        }

        #region Delegates
        private static void _nullLoggerEngineExceptionWrite(Exception exception, string styles, ILoggingContext context) { }
        private static void _defaultLoggerEngineExceptionWrite(Exception exception, string styles, ILoggingContext context) => Logger.Write(exception, styles, context);
        private static void _localLoggerEngineExceptionWrite(Exception exception, string styles, ILoggingContext context) => _localEngine?.Write(exception, styles, context);
        private static void _bothLoggerEngineExceptionWrite(Exception exception, string styles, ILoggingContext context)
        {
            _localEngine?.Write(exception, styles, context);
            Logger.Write(exception, styles: styles, context: context);
        }
        private static void _localFirstLoggerEngineExceptionWrite(
            Exception exception, string styles, ILoggingContext context)
        {
            var local = _localEngine;
            if (local != null)
                local.Write(exception, styles, context);
            else
                Logger.Write(exception, styles: styles, context: context);
        }

        private static void _nullLoggerEngineStringWrite(string message, string styles, ILoggingContext context) { }
        private static void _defaultLoggerEngineStringWrite(string message, string styles, ILoggingContext context) => Logger.Write(message, styles: styles, context: context, category: null);
        private static void _localLoggerEngineStringWrite(string message, string styles, ILoggingContext context) => _localEngine?.Write(message, styles, context, null, 0, null);
        private static void _bothLoggerEngineStringWrite(string message, string styles, ILoggingContext context)
        {
            _localEngine?.Write(message, styles, context, null, 0, null);
            Logger.Write(message, styles: styles, context: context, category: null);
        }
        private static void _localFirstLoggerEngineStringWrite(
            string message, string styles, ILoggingContext context)
        {
            var local = _localEngine;
            if (local != null)
                local.Write(message, styles, context, null, 0, null);
            else
                Logger.Write(message, styles: styles, context: context, category: null);
        }

        private static void _nullLoggerEngineFeed(int repeat) { }
        private static void _defaultLoggerEngineFeed(int repeat) => Logger.Feed(repeat);
        private static void _localLoggerEngineFeed(int repeat) => _localEngine?.Feed(repeat);
        private static void _bothLoggerEngineFeed(int repeat)
        {
            _localEngine?.Feed(repeat);
            Logger.Feed(repeat);
        }
        private static void _localFirstLoggerEngineFeed(int repeat)
        {
            var local = _localEngine;
            if (local != null)
                local.Feed(repeat);
            else
                Logger.Feed(repeat);
        }
        #endregion

        #region ILoggerEngine Implementations
        public void Dispose() { }

        public void Feed(int repeat) => _loggerFeedMethod(repeat);

        public void Write(string @string, string styles, ILoggingContext context,
            string callerMemberName,
            int callerLineNumber,
            string callerFilePath) => _loggerStringWriteMethod(@string, styles, context);

        public void Write(Exception exception, string styles, ILoggingContext context,
            string callerMemberName,
            int callerLineNumber,
            string callerFilePath) => _loggerExceptionWriteMethod(exception, styles, context);
        
        #endregion

        public void Write(string log, string category)
        {
            _loggerStringWriteMethod(log, null, new DefaultLoggingContext(new Dictionary<string, object>
            {
                { LoggerOption.LoggerOptionCategory, category }
            }));
        }
    }
}