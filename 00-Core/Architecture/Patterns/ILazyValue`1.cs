namespace AMKsGear.Architecture.Patterns
{
    /// <inheritdoc cref="ILazyValue"/>
    /// <typeparam name="T">Target value type.</typeparam>
    public interface ILazyValue<T> : ILazyValue
    {
        new T GetValue();
    }
}