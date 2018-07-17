namespace AMKsGear.Architecture.Patterns
{
    /// <summary>
    /// Provides access to a value when needed.
    /// </summary>
    public interface ILazyValue
    {
        object GetValue();
    }
}