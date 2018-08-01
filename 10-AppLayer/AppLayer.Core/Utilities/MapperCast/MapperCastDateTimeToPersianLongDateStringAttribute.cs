//using System;
//using AMKsGear.AppLayer.Core.Localization.Persian.PersianDateTime;
//
//namespace AMKsGear.AppLayer.Core.Utilities.MapperCast
//{
//    public class MapperCastDateTimeToPersianLongDateStringAttribute : MapperCastAttribute
//    {
//        public MapperCastDateTimeToPersianLongDateStringAttribute() : base(typeof(DateTime), x =>
//        {
//            var dateTime = (DateTime)x;
//            return dateTime.ToPersianDateTime().ToShortDateString();
//        })
//        { }
//    }
//}