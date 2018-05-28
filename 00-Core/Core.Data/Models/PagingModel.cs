using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Core.Data.Models
{
    public interface IPagingModel : IModel
    {
        int? PageSize { get; set; }
        int? Page { get; set; }
    }

    public class PagingModel : IPagingModel
    {
        public int? PageSize { get; set; }
        public int? Page { get; set; }
    }
}