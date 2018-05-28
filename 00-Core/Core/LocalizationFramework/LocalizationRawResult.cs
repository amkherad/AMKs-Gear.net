namespace AMKsGear.Core.LocalizationFramework
{
    public class LocalizationRawResult : LocalizableResult
    {
        public string Result { get; }

        public LocalizationRawResult(string result)
        {
            Result = result;
        }
        public override string GetResult() => Result;
    }
}