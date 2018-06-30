//using System;
//using AMKsGear.Architecture.Trace;
//
//namespace AMKsGear.Web.Core.ErrorHandling
//{
//    public enum WebAutoLoggerLoggingHost
//    {
//        DefaultLogger,
//        LocalLogger
//    }
//    public static class WebAutoLogger
//    {
//        public static ILoggerEngine LocalEngine { get; set; }
//        private static Action<Exception> _loggerMethod = _defaultLoggerEngine; 
//
//        public static void Attach()
//        {
//            var httpContext = HttpContext.Current;
//            var application = httpContext.ApplicationInstance;
//
//            application.Error += Application_Error;
//        }
//        public static void Detach()
//        {
//            var httpContext = HttpContext.Current;
//            var application = httpContext.ApplicationInstance;
//
//            application.Error -= Application_Error;
//        }
//
//        private static void Application_Error(object sender, System.EventArgs e)
//        {
//            var exception = HttpContext.Current.Error;
//            if (exception == null) return;
//            _loggerMethod(exception);
//        }
//
//
//
//
//
//        private static WebAutoLoggerLoggingHost _webAutoLoggerLoggingHost = WebAutoLoggerLoggingHost.DefaultLogger;
//        public static WebAutoLoggerLoggingHost WebAutoLoggerLoggingHost
//        {
//            get { return _webAutoLoggerLoggingHost; }
//            set
//            {
//                switch (value)
//                {
//                    case WebAutoLoggerLoggingHost.DefaultLogger:
//                        _loggerMethod = _defaultLoggerEngine;
//                        break;
//                    case WebAutoLoggerLoggingHost.LocalLogger:
//                        _loggerMethod = _localLoggerEngine;
//                        break;
//                    default:
//                        throw new ArgumentOutOfRangeException(nameof(value), value, null);
//                }
//                _webAutoLoggerLoggingHost = value;
//            }
//        }
//
//        private static void _defaultLoggerEngine(Exception exception) => Logger.Write(exception);
//        private static void _localLoggerEngine(Exception exception) => LocalEngine?.Write(exception, null, DefaultLoggingContext.FromCurrentContext());
//    }
//}