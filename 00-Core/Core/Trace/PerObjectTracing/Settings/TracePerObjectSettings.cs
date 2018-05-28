namespace AMKsGear.Core.Trace.PerObjectTracing.Settings
{
    internal class TracePerObjectSettings : TraceSettings
    {
        public static readonly TracePerObjectSettings Default = new TracePerObjectSettings();

        public TracePerObjectSettings()
        {
            LogEngine = Logger.DefaultLogger;
            //Category = null;
            CategoryOverrideStrategy = LogCategoryOverrideStrategy.UseBoth;
        }
    }
}