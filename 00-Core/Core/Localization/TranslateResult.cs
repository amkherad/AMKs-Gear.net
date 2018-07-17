using AMKsGear.Architecture.Patterns;

namespace AMKsGear.Core.Localization
{
    /// <summary>
    /// Provides a lazy access to translation value. 
    /// </summary>
    public abstract class TranslateResult : ITranslateResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract string GetResult();
        
        public virtual object GetValue() => GetResult();
        string ILazyValue<string>.GetValue() => GetResult();

        public override string ToString() => GetResult();

        public static implicit operator string(TranslateResult result) => result?.GetResult() ?? null;
        public static implicit operator TranslateResult(string result) => new TranslateRawResult(result);
    }
}