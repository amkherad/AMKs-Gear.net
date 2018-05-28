using System;
using AMKsGear.AppLayer.Core.Globalization.Persian.PersianDateTime;
using AMKsGear.Core.Automation.Object.Mapper.Annotations;

namespace AMKsGear.AppLayer.Core.Utilities.MapperCast
{
    public class MapperCastDateTimeToPersianShortDateTimeStringAttribute : MapperCastAttribute
    {
        public MapperCastDateTimeToPersianShortDateTimeStringAttribute() : base(typeof(DateTime), x =>
        {
            var dateTime = (DateTime)x;
            return dateTime.ToPersianDateTime().ToLongDateTimeString();
        })
        { }
    }
}