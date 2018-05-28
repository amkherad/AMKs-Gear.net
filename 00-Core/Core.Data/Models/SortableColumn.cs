using AMKsGear.Architecture;

namespace AMKsGear.Core.Data.Models
{
    public class SortableColumn
    {
        public string Name { get; protected set; }
        public SortingOrder Order { get; protected set; }

        public SortableColumn(string name)
        {
            Name = name;
            Order = SortingOrder.Ascending;
        }
        public SortableColumn(string name, SortingOrder order)
        {
            Name = name;
            Order = order;
        }
    }
}