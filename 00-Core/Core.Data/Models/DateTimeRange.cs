//using System;
//using AMKsGear.Architecture.Data.Schema;
//
//namespace AMKsGear.Core.Data.Models
//{
//    public class DateTimeRange : IDateTimeRange
//    {
//        public DateTime? Min { get; set; }
//        public DateTime? Max { get; set; }
//
//        public TimeSpan? From { get; set; }
//        public TimeSpan? To { get; set; }
//
//        public DateTimeRange()
//        {
//        }
//
//        public bool IsIn(DateTime dateTime)
//        {
//            if (Min != null && dateTime < Min) return false;
//            if (Max != null && dateTime < Min) return false;
//
//            return true;
//        }
//
//        public bool IsIn(TimeSpan timeSpan)
//        {
//        }
//    }
//}