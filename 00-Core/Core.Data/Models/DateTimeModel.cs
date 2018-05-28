using System;
using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Core.Data.Models
{
    public interface IDateTimeModel : IModel
    {
        DateTime? DateTime { get; set; }
    }

    public class DateTimeModel : IDateTimeModel
    {
        public DateTime? DateTime { get; set; }
    }
}