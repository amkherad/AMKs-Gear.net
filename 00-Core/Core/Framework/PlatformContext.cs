using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using AMKsGear.Architecture.Platform;

namespace AMKsGear.Core.Framework
{
    public static class PlatformContext
    {
        //private static readonly object PlatformFindoutLock = new object();
        private static IPlatform _platform;
        private static int _initializeCount = 0;
        //private static PlatformType? _platformType;
        
        internal static void Initialize(IPlatform platform)
        {
            if(Interlocked.Increment(ref _initializeCount) > 1) throw new InvalidOperationException("Initialize must be called once per application.");

            _platform = platform;
        }

        public static IPlatform Current => _platform; // ?? TryFillCurrentPlatform();

        //[MethodImpl(MethodImplOptions.NoInlining)]
        //private static IPlatform TryFillCurrentPlatform()
        //{
        //    if (_platform != null) return _platform;
        //    lock (PlatformFindoutLock)
        //    {
        //        if (_platform != null)
        //            return _platform;
        //
        //
        //    }
        //    return _platform;
        //}
    }

    public static class PlatformExtensions
    {
//        [MethodImpl(MethodImplOptions.NoInlining)]
//        public static string GetApplicationHost(this IPlatform platform)
//        {
//            return "";
//        }
//
//        [MethodImpl(MethodImplOptions.NoInlining)]
//        public static Assembly GetPlatformAssembly(this IPlatform platform)
//        {
//            if (platform == null) throw new ArgumentNullException(nameof(platform));
//            var entryType = platform.ApplicationEntryType;
//            return entryType?.Assembly;
//        }
//        
//        [MethodImpl(MethodImplOptions.NoInlining)]
//        public static string GetPlatformAssemblyTitle(this IPlatform platform)
//        {
//            if (platform == null) throw new ArgumentNullException(nameof(platform));
//            var entryType = platform.ApplicationEntryType;
//            return entryType?.Assembly.GetAssemblyTitle();
//        }
    }
}