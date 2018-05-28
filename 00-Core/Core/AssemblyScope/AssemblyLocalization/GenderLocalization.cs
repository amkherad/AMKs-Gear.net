using AMKsGear.Architecture.LocalizationFramework;

namespace AMKsGear.Core.AssemblyScope.AssemblyLocalization
{
    public interface IGenderLocalization : ILocalization
    {
        string GenderMale { get; }
        string GenderFemale { get; }
        string GenderUnspecified { get; }
    }
    public class DefaultGenderLocalization : IGenderLocalization
    {
        public const string DefaultGenderMale = "Male";
        public const string DefaultGenderFemale = "Female";
        public const string DefaultGenderUnspecified = "Unspecified";

        public string GenderMale => DefaultGenderMale;
        public string GenderFemale => DefaultGenderFemale;
        public string GenderUnspecified => DefaultGenderUnspecified;
    }
}