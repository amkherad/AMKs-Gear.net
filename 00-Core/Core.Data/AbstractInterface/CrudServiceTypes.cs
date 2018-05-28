using System.Collections.Generic;
using AMKsGear.Architecture.Data;

namespace AMKsGear.Core.Data.AbstractInterface
{
    public delegate void CrudServiceChangeDetected<TEntity, TOptions>(ICrudService<TEntity, TOptions> sender, TEntity entity)
        where TEntity : IEntity
        where TOptions : ICrudServiceOptions;
    public delegate void CrudServiceBatchActionDetected<TEntity, TOptions>(ICrudService<TEntity, TOptions> sender, IEnumerable<TEntity> entities)
        where TEntity : IEntity
        where TOptions : ICrudServiceOptions;

}