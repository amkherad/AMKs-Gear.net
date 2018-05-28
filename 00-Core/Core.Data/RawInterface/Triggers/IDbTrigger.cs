namespace AMKsGear.Core.Data.RawInterface.Triggers
{
    public interface IDbTrigger
    {
        string Name { get; }
        string Event { get; }
        string Body { get; }
    }
}