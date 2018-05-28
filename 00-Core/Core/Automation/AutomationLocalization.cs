using AMKsGear.Architecture.LocalizationFramework;

namespace AMKsGear.Core.Automation
{
    public interface IAutomationLocalization : ILocalization
    {
        string ThereIsMoreThanOneEnumerableImplementationInDestinationType { get; }
        string UnableToFillDestinationCollection { get; }
        string InvalidNumberOfGenericArguments { get; }
        string UnableToCreateAnInstanceOfDestination { get; }
        string MapperException { get; }
    }
    public class DefaultAutomationLocalization : IAutomationLocalization
    {
        public string ThereIsMoreThanOneEnumerableImplementationInDestinationType => "There's more than one enumerable implementation in destination type";
        public string UnableToFillDestinationCollection => "Unable to fill destination collection.";
        public string InvalidNumberOfGenericArguments => "Invalid number of generic arguments.";
        public string UnableToCreateAnInstanceOfDestination => "Unable to create an instance of destination type using TypeResolver.CreateInstance()";
        public string MapperException => "A mapper exception has been thrown.";
    }
}