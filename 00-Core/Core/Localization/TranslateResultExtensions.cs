namespace AMKsGear.Core.Localization
{

    public static class TranslateResultExtensions
    {
        public static ITranslateResult ToLocalizableResult(this string str) => new TranslateRawResult(str);
    }
}