namespace AMKsGear.Core.Localization
{
    public class TranslateRawResult : TranslateResult
    {
        public string Result { get; }

        public TranslateRawResult(string result)
        {
            Result = result;
        }
        public override string GetResult() => Result;
    }
}