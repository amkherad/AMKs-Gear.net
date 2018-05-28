namespace AMKsGear.Core.Data.Annotations
{
    public class CopyValueOnCreateAttribute : CopyValueAttribute
    {
        public CopyValueOnCreateAttribute(string sourceFieldName)
            : base(CrudActions.Create, sourceFieldName)
        {
            Event = CrudActions.Create;
        }
    }
}