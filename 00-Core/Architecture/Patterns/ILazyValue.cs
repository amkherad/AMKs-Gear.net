namespace AMKsGear.Architecture.Patterns
{
    public interface ILazyValue
    {
        object GetValue();
    }
    public interface ILazyValue<T> : ILazyValue
    {
        new T GetValue();
    }
    public interface ILazyValueGeneric<T>
    {
        T GetValue();
    }
}