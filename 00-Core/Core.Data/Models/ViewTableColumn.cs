using System;
using AMKsGear.Architecture;
using AMKsGear.Architecture.Data.Types;

namespace AMKsGear.Core.Data.Models
{
    public class ViewTableColumn
    {
        public string Name { get; set; }
        public string Data { get; set; }
        public string[] Filters { get; set; }
        public StringCompare CompareMode { get; set; }
        public StringComparison Comparision { get; set; }
        public SortingOrder Order { get; set; }

        public ViewTableColumn() { Order = SortingOrder.Unspecified; }
        public ViewTableColumn(string name)
        {
            Name = name;
            Order = SortingOrder.Unspecified;
        }
    }
}