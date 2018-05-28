using AMKsGear.Architecture.Patterns;

namespace AMKsGear.Core.LocalizationFramework
{
    public interface ILocalizableResult : ILazyValue
    {
        string GetResult();
    }
    public abstract class LocalizableResult : ILocalizableResult
    {
        public const string NoResult = null;

        public abstract string GetResult();
        public virtual object GetValue() => GetResult();
        public override string ToString() => GetResult();

        public static implicit operator string(LocalizableResult result) => result?.GetResult() ?? NoResult;
        public static implicit operator LocalizableResult(string result) => new LocalizationRawResult(result);
    }

    public static class LocalizableResultExtensions
    {
        public static ILocalizableResult ToLocalizableResult(this string str) => new LocalizationRawResult(str);
    }
}