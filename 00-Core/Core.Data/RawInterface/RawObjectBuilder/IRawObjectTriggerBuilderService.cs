using AMKsGear.Architecture.Annotations;
using AMKsGear.Core.Data.RawInterface.Triggers;

namespace AMKsGear.Core.Data.RawInterface.RawObjectBuilder
{
    public interface IRawObjectTriggerBuilderService : IRawObjectBuilderService
    {
        IDbTrigger CreateBoundFieldTriggerQuery(
            [CanBeNull] IRawObjectBuilderContext context,
            string name,
            CrudActions @event,
            //string databaseName,
            string tableName,
            string[] identityFieldNames,
            string destinationFieldName,
            string sourceFieldName,
            bool addExistenceCheck);
    }
}