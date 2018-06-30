namespace AMKsGear.Web.Core.ClientLibraryHelpers
{
    public interface IHtmlTargetSelector
    {
        string BuildSelectorFor(string name);
        string BuildSelectorFor(params object[] args);
    }

    public static class HtmlTargetSelectorExtensions
    {
        public static string BuildSelector(this IHtmlTargetSelector targetSelector, string name)
        {
            return (targetSelector ?? new RawVariableTargetSelector()).BuildSelectorFor(name);
        }

        public static string BuildSelector(this IHtmlTargetSelector targetSelector, params object[] args)
        {
            return (targetSelector ?? new RawVariableTargetSelector()).BuildSelectorFor(args);
        }
    }
}