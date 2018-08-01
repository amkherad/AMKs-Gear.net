//using System;
//using AMKsGear.AppLayer.Core.Localization.Persian.PersianDateTime;
//
//namespace AMKsGear.AppLayer.Core.Utilities.MapperCast
//{
//    public class MapperCastDateTimeToPersianShortDateTimeStringAttribute : MapperCastAttribute
//    {
//        public MapperCastDateTimeToPersianShortDateTimeStringAttribute() : base(typeof(DateTime), x =>
//        {
//            var dateTime = (DateTime)x;
//            return dateTime.ToPersianDateTime().ToLongDateTimeString();
//        })
//        { }
//    }
//}