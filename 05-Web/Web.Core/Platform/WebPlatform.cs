using System;
using AMKsGear.Architecture.Framework;
using AMKsGear.Architecture.Platform;
using AMKsGear.Core.Framework;

namespace AMKsGear.Web.Core.Platform
{
    public class WebPlatform : IPlatform
    {
        private static readonly WebPlatform Platform = new WebPlatform();
        static WebPlatform()
        {
            PlatformContext.Initialize(Platform);
        }
        public static WebPlatform CurrentPlatform() { return Platform; }

        public static void Initialize()
        {
            
        }

        public object GetUnderlyingContext() { return null; }
        public PlatformType Type { get { return PlatformType.Web; } }
        public string Name { get { return FrameworkMembers.WebCoreName; } }
        public string Description { get { return "WebPlatform"; } }

        public string Host { get { return "IIS"; } }
        public Type EntryType { get; }
        public object EntryIfAvailable { get; }

        public object ApplicationEntryIfAvailable
        {
            get
            {
//                var context = HttpContext.Current;
//                if (context == null) return null;
//                var appInstance = context.ApplicationInstance;
//                return appInstance;
                return null;
            }
        }
        public Type ApplicationEntryType
        {
            get
            {
                var entry = ApplicationEntryIfAvailable;
                return entry == null ? null : entry.GetType();
            }
        }
    }
}