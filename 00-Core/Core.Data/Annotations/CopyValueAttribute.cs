namespace AMKsGear.Core.Data.Annotations
{
    public abstract class CopyValueAttribute : TriggerAttribute
    {
        public const string TableNamePlaceHolder = "$Table";
        public const string SourceFieldNamePlaceHolder = "$Source";
        public const string DestinationFieldNamePlaceHolder = "$Destination";

        public string SourceFieldName { get; }

        public string Expression { get; set; }

        public CopyValueAttribute(CrudActions @event, string sourceFieldName)
            : base(@event)
        {
            TriggerName = $"CopyValueOn{@event}{TableNamePlaceHolder}From{SourceFieldNamePlaceHolder}To{DestinationFieldNamePlaceHolder}";
            SourceFieldName = sourceFieldName;
        }

        public string GetExpression()
        {
            var exp = Expression;
            var src = SourceFieldName;
            return exp == null
                ? src
                : exp?.Replace(SourceFieldNamePlaceHolder, src);
        }
    }
}