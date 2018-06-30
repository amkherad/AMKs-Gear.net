namespace AMKsGear.Web.Core.ClientLibraryHelpers
{
    public class RawVariableTargetSelector : IHtmlTargetSelector
    {
        public string BuildSelectorFor(string name) { return name; }
        public string BuildSelectorFor(params object[] args) { return string.Join("", args); }
    }
}