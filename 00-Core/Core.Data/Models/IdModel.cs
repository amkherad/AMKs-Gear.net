using System;
using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Core.Data.Models
{
    public class IdModel<TId> : IIdModel<TId>
    {
        //[Required]
        public virtual TId Id { get; set; }
    }
    public class Int32IdModel : IdModel<int> { }
    public class Int64IdModel : IdModel<long> { }
    public class GuidIdModel : IdModel<Guid> { }
    public class StringIdModel : IdModel<string> { }
}