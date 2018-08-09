using System;
using System.Runtime.CompilerServices;
using AMKsGear.Architecture.Trace;

namespace AMKsGear.Core.Trace
{
    public static class Logger
    {
        private static volatile ILogChannel _defaultChannel;

        public static ILogChannel Default => _defaultChannel ?? throw new InvalidOperationException();

        public static void RegisterDefaultLogChannel(ILogChannel logChannel)
        {
            _defaultChannel = logChannel;
        }

        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Log<TValue>(this ILogChannel channel,
            TValue value,
            [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerFilePath] string callerFilePath = null)
        {
            if (channel == null) throw new ArgumentNullException(nameof(channel));
            channel.LogString(value?.ToString(), null, callerMemberName, callerLineNumber, callerFilePath);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LogString(this ILogChannel channel,
            string @string,
            [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerFilePath] string callerFilePath = null)
        {
            //if (channel == null) throw new ArgumentNullException(nameof(channel));
            channel.LogString(@string, null, callerMemberName, callerLineNumber, callerFilePath);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LogException(this ILogChannel channel,
            Exception exception,
            [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerFilePath] string callerFilePath = null)
        {
            //if (channel == null) throw new ArgumentNullException(nameof(channel));
            channel.LogException(@exception, null, callerMemberName, callerLineNumber, callerFilePath);
        }
    }
}