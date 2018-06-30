using System;

namespace AMKsGear.Web.Core.ClientLibraryHelpers
{
    public class JquerySelector : IHtmlTargetSelector
    {
        public SelectorType TargetSelectorType { get; }

        public JquerySelector() { TargetSelectorType = SelectorType.Minified; }
        public JquerySelector(SelectorType type)
        {
            TargetSelectorType = type;
        }

        public string BuildSelectorFor(string name)
        {
            switch (TargetSelectorType)
            {
                case SelectorType.Minified:
                    return $"$('#{name}')";
                case SelectorType.UseJqueryObject:
                    return $"$('#{name})'";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public string BuildSelectorFor(params object[] args)
        {
            throw new NotSupportedException();
        }

        public enum SelectorType
        {
            Minified,
            UseJqueryObject,
        }
    }
}