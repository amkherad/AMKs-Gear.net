namespace AMKsGear.Core.Data.RawInterface.StoredProcedures
{
    public interface IStoredProcedure
    {
        string Name { get; }
        string Body { get; }
    }
}