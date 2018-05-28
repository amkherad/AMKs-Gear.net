using AMKsGear.AppLayer.Core.Globalization.Persian.PersianLocalization;
using AMKsGear.Architecture.Data.Types;
using AMKsGear.Core.AssemblyScope.AssemblyLocalization;
using AMKsGear.Core.Automation.Object.Mapper.Annotations;
using AMKsGear.Core.LocalizationFramework;

namespace AMKsGear.AppLayer.Core.Globalization.Persian.Utilities.MapperCast
{
    public class MapperCastGenderToPersianStringAttribute : MapperCastAttribute
    {
        public MapperCastGenderToPersianStringAttribute(bool useConstant = false) : base(typeof(Gender), x =>
        {
            var gender = (Gender)x;
            switch (gender)
            {
                case Gender.Male:
                    return useConstant
                        ? PersianGenderLocalization.DefaultGenderMale
                        : (string)Localization.Format<IGenderLocalization, PersianGenderLocalization>(l => l.GenderMale);
                case Gender.Female:
                    return useConstant
                        ? PersianGenderLocalization.DefaultGenderFemale
                        : (string)Localization.Format<IGenderLocalization, PersianGenderLocalization>(l => l.GenderFemale);
                default:
                    return useConstant
                        ? PersianGenderLocalization.DefaultGenderUnspecified
                        : (string)Localization.Format<IGenderLocalization, PersianGenderLocalization>(l => l.GenderUnspecified);
            }
        }) { }
    }
}