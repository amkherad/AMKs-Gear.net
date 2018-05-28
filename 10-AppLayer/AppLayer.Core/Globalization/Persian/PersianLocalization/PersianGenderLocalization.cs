using AMKsGear.Core.AssemblyScope.AssemblyLocalization;

namespace AMKsGear.AppLayer.Core.Globalization.Persian.PersianLocalization
{
    public class PersianGenderLocalization : IGenderLocalization
    {
        public const string DefaultGenderMale = "مذکر";
        public const string DefaultGenderFemale = "مونث";
        public const string DefaultGenderUnspecified = "نامشخص";

        public string GenderMale => DefaultGenderMale;
        public string GenderFemale => DefaultGenderFemale;
        public string GenderUnspecified => DefaultGenderUnspecified;
    }
}