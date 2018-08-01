namespace AMKsGear.Architecture.Localization
{
    /// <summary>
    /// A basic model for strong-named localization services.
    /// </summary>
    public interface ILocalizationModel
    {
        /// <summary>
        /// Target language direction.
        /// </summary>
        LanguageScriptDirection Direction { get; }
        
        /// <summary>
        /// Target language iso 3 letter name.
        /// </summary>
        string LanguageIso3LetterName { get; }
    }
}