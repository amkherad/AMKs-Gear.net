using System.Collections.Generic;
using AMKsGear.Architecture.Trace;
using AMKsGear.Core.Framework;

namespace AMKsGear.Core.Trace
{
    public class DefaultLoggingContext : ILoggingContext
    {
        /// <inheritdoc />
        public string PlatformName { get; private set; }

        /// <inheritdoc />
        public string Application { get; private set; }

        /// <inheritdoc />
        public string AppDomain { get; private set; }

        //public string Host { get; private set; }
        //public string Uri { get; private set; }

        /// <inheritdoc />
        public IDictionary<string, object> Options { get; }

        public DefaultLoggingContext()
        {            
        }
        
        public DefaultLoggingContext(IDictionary<string, object> options)
        {
            Options = options;
        }

        public static DefaultLoggingContext FromCurrentContext()
        {
            var platform = PlatformContext.Current;
            if (platform == null) return null;

            //var entryAssembly = platform.GetPlatformAssembly();

            return new DefaultLoggingContext
            {
                AppDomain = System.AppDomain.CurrentDomain.FriendlyName,
                //Application = entryAssembly?.GetAssemblyTitle(),
//                        Host = platform.Host,
//                        Uri = entryAssembly?.Location,

                PlatformName = platform.Name
            };
        }
    }
}