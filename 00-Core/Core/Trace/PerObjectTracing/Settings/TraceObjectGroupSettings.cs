namespace AMKsGear.Core.Trace.PerObjectTracing.Settings
{
    internal abstract class TraceObjectGroupSettings : TraceSettings
    {
        public TraceObjectGroupSettings()
        {
            LogEngine = Logger.DefaultLogger;
            //Category = null;
            CategoryOverrideStrategy = LogCategoryOverrideStrategy.NoOverride;
        }

        public abstract bool IsMemberOfGroup(object obj);
    }
}