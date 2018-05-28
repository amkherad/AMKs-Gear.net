namespace AMKsGear.Core.Data.Models
{
    public class FilterableColumn
    {
        public string Name { get; protected set; }
        public string Filter { get; protected set; }

        public FilterableColumn(string name)
        {
            Name = name;
            Filter = string.Empty;
        }
        public FilterableColumn(string name, string filter)
        {
            Name = name;
            Filter = filter;
        }
    }
}