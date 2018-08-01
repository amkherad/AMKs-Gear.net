namespace AMKsGear.Architecture.Trace
{
    /// <summary>
    /// Specifies the level of the information sent to log channel.
    /// </summary>
    public enum LogLevel
    {
        Disable = 0,
        Informational = 1,
        Track = 2,
        Debug = 3,
        HighestAvailable = 10
    }
}