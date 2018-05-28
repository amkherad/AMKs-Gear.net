using System.Collections.Generic;
using System.Reflection;
using AMKsGear.Core.Trace;

namespace AMKsGear.Core.TraceTools
{
    public static partial class Test
    {
        internal static LocalLogger Logger;


        public static void RegisterPrivateLogger(LocalLogger localLogger) => Logger = localLogger;


        public static void RunUnitTests() { }
        public static void RunUnitTests(IEnumerable<Assembly> assemblies)
        {

        }
    }
}