using System.Collections.Generic;
using AMKsGear.Architecture.Data;

namespace AMKsGear.Core.Data.Utilities
{
    public class IdEntityEqualityComparer<TEntity, TId> : IEqualityComparer<TEntity>
        where TEntity : IIdEntity<TId>
    {
        public bool Equals(TEntity x, TEntity y) => x.Id.Equals(y.Id);
        public int GetHashCode(TEntity obj) => obj.Id.GetHashCode();
    }
}
