namespace AMKsGear.Core.Automation.Mapper
{
    /// <summary>
    /// <see cref="MapperContext"/> merge behavior.
    /// </summary>
    public enum MapperContextMergeBehavior
    {
        /// <summary>
        /// Overwrites all existing rows with duplicates in new set.
        /// </summary>
        OverwriteDuplicates,
        
        /// <summary>
        /// Skips all duplicates and keep old row.
        /// </summary>
        SkipDuplicates,
        
        /// <summary>
        /// Throws exception on any duplicity. (rolls back all inserted rows)
        /// </summary>
        ThrowException //default
    }
}