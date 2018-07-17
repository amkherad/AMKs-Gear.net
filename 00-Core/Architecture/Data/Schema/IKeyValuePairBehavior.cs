namespace AMKsGear.Architecture.Data.Schema
{
    public interface IKeyValuePairBehavior<out TKey, out TValue>
    {
        TKey Key { get; }
        TValue Value { get; }
    }
}