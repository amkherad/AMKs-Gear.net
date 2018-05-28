using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AMKsGear.Architecture.Trace;
using AMKsGear.Core.Trace.LoggerEngines;
using AMKsGear.Core.Collections;
using AMKsGear.Core.Trace.PerObjectTracing;
using AMKsGear.Core.Utils;

// ReSharper disable ExplicitCallerInfoArgument
// ReSharper disable MethodOverloadWithOptionalParameter

namespace AMKsGear.Core.Trace
{
    public class LoggerContext
    {
        public ILoggerEngine Engine;
        public string Category;

        internal LoggerContext Clone() =>
            new LoggerContext
            {
                Category = Category,
                Engine = Engine
            };
    }
    public static class Logger
    {
        #region General
        public static event EventHandler<ExceptionEventArgs> LoggerErrorOccured;

        public static readonly object LoggerSyncLock = new object();
        private static volatile bool _loggingState = true;
        internal static readonly DebuggerLogger DefaultLogger = new DebuggerLogger();
        private static volatile List<LoggerContext> _activeLoggers = new List<LoggerContext>();

        public const string NoStyle = "[Reset]";
        public const string SuccessStyle = "[Success]";
        public const string BoldStyle = "[Bold]";
        public const string ErrorStyle = "[Error]";
        public const string WarningStyle = "[Warning]";

        public static ILoggerEngine[] ActiveLoggers
        {
            get
            {
                //lock (LoggerSyncLock)
                //removed for performance, no thread-safety required most of the time, for critical usages 
                {
                    var loggers = _activeLoggers.ToArray();
                    return loggers.Length == 0
                        ? new ILoggerEngine[] { new DebuggerLogger() }
                        : loggers.Select(x => x.Engine).ToArray();
                }
            }
        }

        public static void EnableLogging()
        {
            _loggingState = true;
        }
        public static void DisableLogging()
        {
            _loggingState = false;
        }
        
        public static void RegisterLogger(string category, params ILoggerEngine[] loggers)
        {
            if (loggers == null) throw new ArgumentNullException(nameof(loggers));
            if (loggers.Length == 0) throw new ArgumentOutOfRangeException(nameof(loggers));
            lock (LoggerSyncLock)
            {
                foreach (var logger in loggers)
                    _activeLoggers.Add(new LoggerContext
                    {
                        Engine = logger,
                        Category = category
                    });
            }
        }
        public static void RegisterLogger(params LoggerContext[] loggers)
        {
            if (loggers == null) throw new ArgumentNullException(nameof(loggers));
            if (loggers.Length == 0) throw new ArgumentOutOfRangeException(nameof(loggers));
            lock (LoggerSyncLock)
                _activeLoggers.AddRange(loggers.Select(x => x.Clone()));
        }
        public static void RegisterLogger(params ILoggerEngine[] loggers)
        {
            if (loggers == null) throw new ArgumentNullException(nameof(loggers));
            if (loggers.Length == 0) throw new ArgumentOutOfRangeException(nameof(loggers));
            lock (LoggerSyncLock)
                _activeLoggers.AddRange(loggers.Select(x => new LoggerContext {Engine = x}));
        }
        public static void RegisterDefaultLogger() { RegisterLogger((string)null, DefaultLogger); }
        public static bool UnregisterLogger(ILoggerEngine logger)
        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));
            lock (LoggerSyncLock)
            {
                return _activeLoggers.RemoveAll(x => x.Engine == logger) > 0;
            }
        }

        internal static void OnLoggerErrorOccured(object @this, ExceptionEventArgs e)
        {
            LoggerErrorOccured?.Invoke(@this, e);
        }

        public static class Styles
        {
            public const string Bold = "Bold";
            public const string Highlight = "Highlight";
            public const string Block = "Block";
        }
        #endregion

        #region Writers
        public static void Write(object @object, string styles = null, string category = null,
            [CallerMemberName] string callerMemberName = null)
            => _write(@object.ToString(), styles: styles, category: category, callerMemberName: callerMemberName);
        public static void Write(string message) => _write(message);
        public static void Here([CallerMemberName] string callerMemberName = null, [CallerLineNumber] int callerLineNumber = 0, [CallerFilePath] string callerFilePath = null)
        { _write($"method: {callerMemberName}, line: {callerLineNumber}, filePath: {callerFilePath}", callerMemberName: callerMemberName); }

        public static void Write(string @string, string category = null, [CallerMemberName] string callerMemberName = null) 
           => _write(@string, category: category, callerMemberName: callerMemberName);
        //public static void Write<T>(T @string, string category = null)
        //    => _write(@string.ToString(), category: category);
        //public static void Write<T>(T @string) => _write(@string.ToString());
        //public static void Write<T>(T exception, string styles, ILoggingContext context, string category = null, [CallerMemberName] string callerMemberName = null) =>
        //    _write(exception.ToString(), styles: styles, context: context, category: category, callerMemberName: callerMemberName);
        public static void Write(Exception exception, string styles, ILoggingContext context, string category = null, [CallerMemberName] string callerMemberName = null) =>
            _write(exception.Message, styles: styles, context: context, category: category, callerMemberName: callerMemberName);

        public static void Write<TElement>(IEnumerable<TElement> elements, string category = null) => elements.ForEach(x => Write(x, category));
        //public static void Write(string @string, string category, object arg0, params object[] args) =>
        //    Write(string.Format(@string,
        //        (args ?? new object[0]).Merge(arg0, RelativeOrder.Before)), null, (ILoggingContext)null, category, null);
        //public static void Write(string @string, object arg0, params object[] args) =>
        //    Write(string.Format(@string,
        //        (args ?? new object[0]).Merge(arg0, RelativeOrder.Before)), null, (ILoggingContext)null, null, null);

        public static void Write(bool @string, string styles, ILoggingContext context, string category = null, [CallerMemberName] string callerMemberName = null) => _write(@string.ToString(), styles, context, category, callerMemberName);
        public static void Write(short @string, string styles, ILoggingContext context, string category = null, [CallerMemberName] string callerMemberName = null) => _write(@string.ToString(), styles, context, category, callerMemberName);
        public static void Write(int @string, string styles, ILoggingContext context, string category = null, [CallerMemberName] string callerMemberName = null) => _write(@string.ToString(), styles, context, category, callerMemberName);
        public static void Write(long @string, string styles, ILoggingContext context, string category = null, [CallerMemberName] string callerMemberName = null) => _write(@string.ToString(), styles, context, category, callerMemberName);
        public static void Write(float @string, string styles, ILoggingContext context, string category = null, [CallerMemberName] string callerMemberName = null) => _write(@string.ToString(), styles, context, category, callerMemberName);
        public static void Write(double @string, string styles, ILoggingContext context, string category = null, [CallerMemberName] string callerMemberName = null) => _write(@string.ToString(), styles, context, category, callerMemberName);
        public static void Write(decimal @string, string styles, ILoggingContext context, string category = null, [CallerMemberName] string callerMemberName = null) => _write(@string.ToString(), styles, context, category, callerMemberName);
        public static void Write(byte @string, string styles, ILoggingContext context, string category = null, [CallerMemberName] string callerMemberName = null) => _write(@string.ToString(), styles, context, category, callerMemberName);
        public static void Write(char @string, string styles, ILoggingContext context, string category = null, [CallerMemberName] string callerMemberName = null) => _write(@string.ToString(), styles, context, category, callerMemberName);
        public static void Write(uint @string, string styles, ILoggingContext context, string category = null, [CallerMemberName] string callerMemberName = null) => _write(@string.ToString(), styles, context, category, callerMemberName);
        public static void Write(ulong @string, string styles, ILoggingContext context, string category = null, [CallerMemberName] string callerMemberName = null) => _write(@string.ToString(), styles, context, category, callerMemberName);
        public static void Write(ushort @string, string styles, ILoggingContext context, string category = null, [CallerMemberName] string callerMemberName = null) => _write(@string.ToString(), styles, context, category, callerMemberName);
        
        public static void Write(string @string, string styles, ILoggingContext context, string category, [CallerMemberName] string callerMemberName = null)
            => _write(@string, styles: styles, context: context, category: category, callerMemberName: callerMemberName);
        private static void _write(string @string, string styles = null, ILoggingContext context = null, string category = null, string callerMemberName = null)
        {
            if (!_loggingState) return;
            ILoggerEngine engine = null;

            if (category == null)
            {
                var options = context?.Options;
                if (options != null)
                {
                    object catOption;
                    options.TryGetValue(LoggerOption.LoggerOptionCategory, out catOption);
                    category = catOption as string;
                }
            }

            try
            {
                var activeLoggers = _activeLoggers.ToArray();
                if (activeLoggers.Length == 0)
                {
                    engine = DefaultLogger;
                    DefaultLogger.Write(@string, styles, context, null, 0, null);
                }
                else
                    foreach (var logger in activeLoggers)
                    {
                        engine = logger.Engine;
                        if (engine == null) continue;
                        if (logger.Category == category)
                            engine.Write(@string, styles, context, null, 0, null);
                    }
            }
            catch (Exception ex)
            {
                OnLoggerErrorOccured(engine, new ExceptionEventArgs(ex));
            }
        }

        public static void WriteLine(string message) => Write(message);

        public static void Feed(int repeat = 1) => Feed(1, null);
        public static void Feed(int repeat = 1, string category = null)
        {
            if (repeat < 0) throw new ArgumentOutOfRangeException(nameof(repeat));
            if (repeat > 1000) throw new ArgumentOutOfRangeException(nameof(repeat));
            if (!_loggingState) return;
            ILoggerEngine engine = null;
            try
            {
                var activeLoggers = _activeLoggers.ToArray();
                if (activeLoggers.Length == 0)
                {
                    engine = DefaultLogger;
                    DefaultLogger.Feed(repeat);
                }
                else
                    foreach (var logger in activeLoggers)
                    {
                        engine = logger.Engine;
                        if (engine == null) continue;
                        if (logger.Category == category)
                            engine.Feed(repeat);
                    }
            }
            catch (Exception ex)
            {
                OnLoggerErrorOccured(engine, new ExceptionEventArgs(ex));
            }
        }
        #endregion


        public static void WritePrivateLog(object source, string message, string category,
            [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerFilePath] string callerFilePath = null)
            => PerObjectTracingHandler.WritePrivateLog(source, message, category, null, callerMemberName, callerLineNumber, callerFilePath);

        public static void WritePrivateLog(object source, string message,
            [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerFilePath] string callerFilePath = null)
            => PerObjectTracingHandler.WritePrivateLog(source, message, null, null, callerMemberName, callerLineNumber, callerFilePath);

        public static void WritePrivateLog(object source, string message, string category, ILoggingContext context,
            [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerFilePath] string callerFilePath = null)
            => PerObjectTracingHandler.WritePrivateLog(source, message, category, context, callerMemberName, callerLineNumber, callerFilePath);
    }
}