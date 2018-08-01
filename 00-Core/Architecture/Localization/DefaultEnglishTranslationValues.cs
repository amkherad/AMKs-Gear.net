namespace AMKsGear.Architecture.Localization
{
    public abstract class DefaultEnglishLocalization : ILocalizationModel
    {
        public static LanguageScriptDirection DefaultDirection = LanguageScriptDirection.LeftToRight;
        public static string DefaultIso3LetterName = "Eng";


        public LanguageScriptDirection Direction => DefaultDirection;
        public string LanguageIso3LetterName => DefaultIso3LetterName;
    }
}