//using AMKsGear.AppLayer.Core.Localization.Persian.PersianLocalization;
//using AMKsGear.Architecture.Data.Types;
//using AMKsGear.Core.AssemblyScope.AssemblyLocalization;
//using AMKsGear.Core.Localization;
//
//namespace AMKsGear.AppLayer.Core.Localization.Persian.Utilities.MapperCast
//{
//    public class MapperCastGenderToPersianStringAttribute : MapperCastAttribute
//    {
//        public MapperCastGenderToPersianStringAttribute(bool useConstant = false) : base(typeof(Gender), x =>
//        {
//            var gender = (Gender)x;
//            switch (gender)
//            {
//                case Gender.Male:
//                    return useConstant
//                        ? PersianGenderLocalizationModel.DefaultGenderMale
//                        : (string)LocalizationServices.Format<IGenderLocalizationModel, PersianGenderLocalizationModel>(l => l.GenderMale);
//                case Gender.Female:
//                    return useConstant
//                        ? PersianGenderLocalizationModel.DefaultGenderFemale
//                        : (string)LocalizationServices.Format<IGenderLocalizationModel, PersianGenderLocalizationModel>(l => l.GenderFemale);
//                default:
//                    return useConstant
//                        ? PersianGenderLocalizationModel.DefaultGenderUnspecified
//                        : (string)LocalizationServices.Format<IGenderLocalizationModel, PersianGenderLocalizationModel>(l => l.GenderUnspecified);
//            }
//        }) { }
//    }
//}