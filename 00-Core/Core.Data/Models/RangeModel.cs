using System;
using AMKsGear.Architecture.Data.Schema;

namespace AMKsGear.Core.Data.Models
{
    public class RangeModel<T> : IRange<T>
    {
        public T Min { get; set; }
        public T Max { get; set; }
    }

    public class Int32RangeModel : RangeModel<int>
    {
        public int Length => Max - Min + 1;
    }
    public class Int64RangeModel : RangeModel<long>
    {
        public long Length => Max - Min + 1;
    }
    public class DateTimeRangeModel : RangeModel<DateTime> { }
}