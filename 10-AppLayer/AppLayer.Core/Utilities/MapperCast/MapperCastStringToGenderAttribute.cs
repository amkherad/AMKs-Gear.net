//using AMKsGear.Architecture.Data.Types;
//using AMKsGear.Core.Utils;
//
//namespace AMKsGear.AppLayer.Core.Utilities.MapperCast
//{
//    public class MapperCastStringToGenderAttribute : MapperCastAttribute
//    {
//        public MapperCastStringToGenderAttribute() : base(typeof(string), x =>
//        {
//            var str = x as string;
//            return str == null ? Gender.Unspecified : Helper.GenderFromString(str);
//        }) { }
//    }
//}