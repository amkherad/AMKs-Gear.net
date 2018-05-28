namespace AMKsGear.Architecture.LifetimeManagers
{
    public interface ITransactionContainer
    {
        ITransaction BeginTransaction();
    }
}