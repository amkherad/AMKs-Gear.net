using System.Collections.Generic;
using AMKsGear.Architecture.Trace;

namespace AMKsGear.Core.Trace
{
    public class DefaultLoggingContext : ILoggingContext
    {
        public string PlatformName { get; private set; }
        public string Application { get; private set; }
        public string AppDomain { get; private set; }
        //public string Host { get; private set; }
        //public string Uri { get; private set; }
        
        public IDictionary<string, object> Options { get; }

        public DefaultLoggingContext(IDictionary<string, object> options)
        {
            Options = options;
        }

        //        public static DefaultLoggingContext FromCurrentContext()
        //        {
        //            var platform = PlatformContext.Current;
        //            if (platform == null) return null;
        //            var entryAssembly = platform.GetPlatformAssembly();
        //            return new DefaultLoggingContext
        //            {
        //                AppDomain = System.AppDomain.CurrentDomain.FriendlyName,
        //                Application = entryAssembly?.GetAssemblyTitle(),
        //                Host = platform.Host,
        //                Uri = entryAssembly?.Location,
        //
        //                PlatformName = platform.Name
        //            };
        //        }
    }
}