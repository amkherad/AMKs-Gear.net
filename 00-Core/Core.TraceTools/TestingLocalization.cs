using AMKsGear.Architecture.AssemblyScope.AssemblyLocalization;
using AMKsGear.Architecture.LocalizationFramework;

namespace AMKsGear.Core.TraceTools
{
    public interface ITestingLocalization : ILocalization, IActionResultLocalization
    {
        string AssertionDoneWithResultOf { get; }
        string TestDoneWithResultOf { get; }
    }

    public class DefaultTestingLocalization : DefaultActionResultLocalization, ITestingLocalization
    {
        public string AssertionDoneWithResultOf { get; } = "Assertion '{0}' done with the result of: {1}";
        public string TestDoneWithResultOf { get; } = "Test '{0}' has been {1}";
    }
}