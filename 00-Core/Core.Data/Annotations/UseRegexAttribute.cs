using AMKsGear.Architecture.Modeling.Annotations;

namespace AMKsGear.Core.Data.Annotations
{
    public class UseRegexAttribute : HintBagAttribute
    {
        public const string ValueKey = "UseRegex";

        public bool UseRegix { get; }

        public UseRegexAttribute() : this(true) { }
        public UseRegexAttribute(bool useRegex)
            : base(ValueKey, useRegex.ToString())
        {
            UseRegix = useRegex;
        }
    }
}