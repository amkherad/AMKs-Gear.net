namespace AMKsGear.Core.Automation.Mapper
{
    /// <summary>
    /// This enum used to determine the level of optimizations allowed for a mapping.
    /// (e.g. the use of Array.Copy should not use if AllowWithRespectToQueryables presented)
    /// </summary>
    public enum MappingOptimizations
    {
        /// <summary>
        /// Skip all optimizations. (Still some optimizations may be used)
        /// </summary>
        NoOptimization,
        
        /// <summary>
        /// Allow all inline optimizations that are supported in queryables. This is the default value
        /// </summary>
        AllowWithRespectToQueryables,
        
        /// <summary>
        /// Force all optimizations. (An exception will be thrown on projection methods)
        /// </summary>
        ForceOptimize
    }
}