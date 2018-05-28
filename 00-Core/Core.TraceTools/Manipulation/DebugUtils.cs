using System;
using System.Threading;

namespace AMKsGear.Core.TraceTools.Manipulation
{
    public static class DebugUtils
    {
        private static Random _random;

        public static Random RandomGenerator
        {
            get
            {
                LazyInitializer.EnsureInitialized(ref _random, () => new Random(Environment.TickCount));
                return _random;
            }
        }
    }
}