using AMKsGear.Architecture.Modeling.Annotations;

namespace AMKsGear.Core.Data.Annotations
{
    public class SearchableAttribute : HintBagAttribute
    {
        public const string ValueKey = "Searchable";

        public bool Searchable { get; }

        public SearchableAttribute() : this(true) { }
        public SearchableAttribute(bool searchable)
            : base(ValueKey, searchable.ToString())
        {
            Searchable = searchable;
        }
    }
}