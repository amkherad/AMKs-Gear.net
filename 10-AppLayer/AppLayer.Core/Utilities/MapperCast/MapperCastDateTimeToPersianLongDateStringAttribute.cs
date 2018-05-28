﻿using System;
using AMKsGear.AppLayer.Core.Globalization.Persian.PersianDateTime;
using AMKsGear.Core.Automation.Object.Mapper.Annotations;

namespace AMKsGear.AppLayer.Core.Utilities.MapperCast
{
    public class MapperCastDateTimeToPersianLongDateStringAttribute : MapperCastAttribute
    {
        public MapperCastDateTimeToPersianLongDateStringAttribute() : base(typeof(DateTime), x =>
        {
            var dateTime = (DateTime)x;
            return dateTime.ToPersianDateTime().ToShortDateString();
        })
        { }
    }
}