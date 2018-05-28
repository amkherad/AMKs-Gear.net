//using System;
//using System.Collections.Generic;
//
//namespace AMKsGear.Architecture.Data.Schema
//{
//    public class DateTimeRangeTimeMask
//    {
//        TimeSpan From { get; set; }
//        TimeSpan To { get; set; }
//        
//        
//    }
//    
//    /// <summary>
//    /// Represents a date time range with a time of day mask.
//    /// </summary>
//    public interface IDateTimeRange : IRange<DateTime?>
//    {
//        IEnumerable<DateTimeRangeTimeMask> TimeMasks { get; set; }
//
//        bool IsIn(DateTime dateTime);
//        bool IsIn(TimeSpan timeSpan);
//    }
//    public interface IDateTimeRangeModel : IDateTimeRange, IRangeModel<DateTime?> { }
//    public interface IDateTimeRangeEntity : IDateTimeRange, IRangeEntity<DateTime?> { }
//}