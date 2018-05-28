using AMKsGear.Architecture.Modeling.Annotations;

namespace AMKsGear.Core.Data.Annotations
{
    public class OrderableAttribute : HintBagAttribute
    {
        public const string ValueKey = "Orderable";

        public bool Orderable { get; }

        public OrderableAttribute() : this(true) { }
        public OrderableAttribute(bool orderable)
            : base(ValueKey, orderable.ToString())
        {
            Orderable = orderable;
        }
    }
}