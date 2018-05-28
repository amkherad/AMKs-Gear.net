using System.Collections;
using System.Collections.Generic;
using AMKsGear.Architecture.Data;

namespace AMKsGear.Core.Data.AbstractInterface
{
    public interface IQueryExecutorCrudService//<TOptions>
        //where TOptions : ICrudServiceOptions
    {
        object RawResultQuery(string query, params object[] args);
        IEnumerable MultiResultQuery(string query, params object[] args);
        object SingleResultQuery(string query, params object[] args);
        TScalar ScalarResultQuery<TScalar>(string query, params object[] args);
    }
    public interface IQueryExecutorCrudService<TEntity, TOptions> : IQueryExecutorCrudService//<TOptions>
        where TEntity : IEntity
        where TOptions : ICrudServiceOptions
    {
        new IEnumerable<TEntity> MultiResultQuery(string query, params object[] args);
        new TEntity SingleResultQuery(string query, params object[] args);
    }
}