//using AMKsGear.Architecture.Data.Types;
//using AMKsGear.Core.Utils;
//
//namespace AMKsGear.AppLayer.Core.Localization.Persian.Utilities.MapperCast
//{
//    public class MapperCastPersianStringToGenderAttribute : MapperCastAttribute
//    {
//        public MapperCastPersianStringToGenderAttribute() : base(typeof(string), x =>
//        {
//            var str = x as string;
//            switch (str)
//            {
//                case "مرد": return Gender.Male;
//                case "آقا": return Gender.Male;
//                case "مذکر": return Gender.Male;
//                case "پسر": return Gender.Male;
//                
//                case "زن": return Gender.Female;
//                case "خانم": return Gender.Female;
//                case "بانو": return Gender.Female;
//                case "مونث": return Gender.Female;
//                case "دختر": return Gender.Female;
//                
//                default:
//                    return str == null ? Gender.Unspecified : Helper.GenderFromString(str);
//            }
//        }) { }
//    }
//}