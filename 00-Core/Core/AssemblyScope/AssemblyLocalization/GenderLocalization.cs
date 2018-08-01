using AMKsGear.Architecture.Localization;

namespace AMKsGear.Core.AssemblyScope.AssemblyLocalization
{
    public interface IGenderLocalizationModel : ILocalizationModel
    {
        string GenderMale { get; }
        string GenderFemale { get; }
        string GenderUnspecified { get; }
    }
    public class DefaultGenderLocalizationModel : DefaultEnglishLocalization, IGenderLocalizationModel
    {
        public const string DefaultGenderMale = "Male";
        public const string DefaultGenderFemale = "Female";
        public const string DefaultGenderUnspecified = "Unspecified";

        public string GenderMale => DefaultGenderMale;
        public string GenderFemale => DefaultGenderFemale;
        public string GenderUnspecified => DefaultGenderUnspecified;
    }
}