using System;
using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Core.Data.Models
{
    public interface IPageDateTimeModel : IModel
    {
        DateTime? DateTime { get; set; }

        int? PageSize { get; set; }
        int? Page { get; set; }
    }

    public class PageDateTimeModel : IPageDateTimeModel
    {
        public DateTime? DateTime { get; set; }

        public int? PageSize { get; set; }
        public int? Page { get; set; }
    }
}