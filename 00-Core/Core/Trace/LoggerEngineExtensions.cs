using System;
using System.Runtime.CompilerServices;
using AMKsGear.Architecture.Trace;

// ReSharper disable ExplicitCallerInfoArgument

namespace AMKsGear.Core.Trace
{
    public static class LoggerEngineExtensions
    {
        public static void Write(this ILoggerEngine engine, string @string, string styles)
        {
            if (engine == null) throw new ArgumentNullException(nameof(engine));
            engine.Write(@string, styles, null);
        }
        public static void Write(this ILoggerEngine engine, string @string, string styles = "", [CallerMemberName] string callerMemberName = null)
        { Write(engine, @string, styles); }


        public static void Write(this ILoggerEngine engine, bool @string, string styles = "", [CallerMemberName] string callerMemberName = null)
        { Write(engine, @string.ToString(), styles, callerMemberName); }
        public static void Write(this ILoggerEngine engine, short @string, string styles = "", [CallerMemberName] string callerMemberName = null)
        { Write(engine, @string.ToString(), styles, callerMemberName); }
        public static void Write(this ILoggerEngine engine, int @string, string styles = "", [CallerMemberName] string callerMemberName = null)
        { Write(engine, @string.ToString(), styles, callerMemberName); }
        public static void Write(this ILoggerEngine engine, long @string, string styles = "", [CallerMemberName] string callerMemberName = null)
        { Write(engine, @string.ToString(), styles, callerMemberName); }
        public static void Write(this ILoggerEngine engine, float @string, string styles = "", [CallerMemberName] string callerMemberName = null)
        { Write(engine, @string.ToString(), styles, callerMemberName); }
        public static void Write(this ILoggerEngine engine, double @string, string styles = "", [CallerMemberName] string callerMemberName = null)
        { Write(engine, @string.ToString(), styles, callerMemberName); }
        public static void Write(this ILoggerEngine engine, decimal @string, string styles = "", [CallerMemberName] string callerMemberName = null)
        { Write(engine, @string.ToString(), styles, callerMemberName); }
        public static void Write(this ILoggerEngine engine, byte @string, string styles = "", [CallerMemberName] string callerMemberName = null)
        { Write(engine, @string.ToString(), styles, callerMemberName); }
        public static void Write(this ILoggerEngine engine, char @string, string styles = "", [CallerMemberName] string callerMemberName = null)
        { Write(engine, @string.ToString(), styles, callerMemberName); }
        public static void Write(this ILoggerEngine engine, uint @string, string styles = "", [CallerMemberName] string callerMemberName = null)
        { Write(engine, @string.ToString(), styles, callerMemberName); }
        public static void Write(this ILoggerEngine engine, ulong @string, string styles = "", [CallerMemberName] string callerMemberName = null)
        { Write(engine, @string.ToString(), styles, callerMemberName); }
        public static void Write(this ILoggerEngine engine, ushort @string, string styles = "", [CallerMemberName] string callerMemberName = null)
        { Write(engine, @string.ToString(), styles, callerMemberName); }
        
        public static void Write(this ILoggerEngine engine, Exception exception, string styles = "",
            ILoggingContext context = null) => engine.Write(exception, styles, context);
    }
}