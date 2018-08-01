using AMKsGear.Architecture.Localization;
using AMKsGear.Core.AssemblyScope.AssemblyLocalization;

namespace AMKsGear.AppLayer.Core.Localization.Persian.PersianLocalization
{
    public class PersianGenderLocalizationModel : DefaultEnglishLocalization, IGenderLocalizationModel
    {
        public const string DefaultGenderMale = "مذکر";
        public const string DefaultGenderFemale = "مونث";
        public const string DefaultGenderUnspecified = "نامشخص";

        public string GenderMale => DefaultGenderMale;
        public string GenderFemale => DefaultGenderFemale;
        public string GenderUnspecified => DefaultGenderUnspecified;
    }
}