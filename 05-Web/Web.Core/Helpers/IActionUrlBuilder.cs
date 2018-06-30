namespace AMKsGear.Web.Core.Helpers
{
    public interface IActionUrlBuilder
    {
        string Action(string action);
        string Action(string action, string controller);
        string Action(string action, string controller, object routeValues);
        string Action(string action, string controller, object routeValues, string protocol);
    }
}