using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ir.amkdp.gear.arch.Automation.IoC;
using ir.amkdp.gear.arch.Automation.IoC.Annotations;
using ir.amkdp.gear.arch.Data;
using ir.amkdp.gear.core.Automation.IoC;
using ir.amkdp.gear.data.AbstractInterface;
using ir.amkdp.gear.data.RawWrapper;

namespace Gear.WinTests
{
    public class CrudOptions<TEntity> : DefaultCrudServiceOptions<TEntity>
    {
        public Func<Expression<Func<TEntity, bool>>> InsertOrUpdateEqualityExpressionCreator { get; set; }
    }
    public interface ICrud<TEntity> : IInt32IdCrudService<TEntity, CrudOptions<TEntity>>
        where TEntity : IIdEntity<int>, new()
    {
    }
    public class Crud<TEntity> : ICrud<TEntity>
        where TEntity : IIdEntity<int>, new()
    {
        public event EventHandler ChangeDetected;
        public event CrudServiceChangeDetected<TEntity, CrudOptions<TEntity>> InsertDetected;
        public event CrudServiceChangeDetected<TEntity, CrudOptions<TEntity>> UpdateDetected;
        public event CrudServiceChangeDetected<TEntity, CrudOptions<TEntity>> DeleteDetected;

        public event CrudServiceBatchActionDetected<TEntity, CrudOptions<TEntity>> BatchInsertDetected;
        public event CrudServiceBatchActionDetected<TEntity, CrudOptions<TEntity>> BatchUpdateDetected;
        public event CrudServiceBatchActionDetected<TEntity, CrudOptions<TEntity>> BatchDeleteDetected;
        public IEnumerable<TEntity> GetAll(CrudOptions<TEntity> options)
        {
            throw new NotImplementedException();
        }

        public object GetService(string serviceName)
        {
            throw new NotImplementedException();
        }

        public object GetService(string serviceName, Type crudType)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TSelect> GetAll<TSelect>(Expression<Func<TEntity, TSelect>> @select, CrudOptions<TEntity> options)
        {
            throw new NotImplementedException();
        }

        public TEntity Find(Expression<Func<TEntity, bool>> predicate, CrudOptions<TEntity> options)
        {
            throw new NotImplementedException();
        }

        public TSelect Find<TSelect>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TSelect>> @select, CrudOptions<TEntity> options)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate, CrudOptions<TEntity> options)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TSelect> FindAll<TSelect>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TSelect>> @select, CrudOptions<TEntity> options)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> Insert(IEnumerable<TEntity> entities, CrudOptions<TEntity> options)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> Update(IEnumerable<TEntity> entities, CrudOptions<TEntity> options)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> InsertOrUpdate(IEnumerable<TEntity> entities, CrudOptions<TEntity> options)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> Delete(Expression<Func<TEntity, bool>> predicate, CrudOptions<TEntity> options)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities, CrudOptions<TEntity> options)
        {
            throw new NotImplementedException();
        }

        //public Table

        [ResolveOrderMax]
        public Crud(IRawWrapper wrapper)
        {
            
        }
        public Crud(ITypeResolver resolver) : this(resolver.Resolve<IRawWrapper>()) { }
        public Crud() : this(TypeResolver.CreateInstance<IRawWrapper>()) { }
        
        
        IEnumerable ICrudService<CrudOptions<TEntity>>.GetAll(CrudOptions<TEntity> options) => GetAll(options);

        public object GetUnderlyingContext() => null;

        //IEnumerable ICrudService<DefaultCrudServiceOptions>.GetAll(CrudOptions<TEntity> options) => Table;

        


        public Expression<Func<TEntity, bool>> CreateEqualityComparerExpression(TEntity other)
            => x => x.Id.Equals(other.Id);

        public TEntity Find(int id, CrudOptions<TEntity> options)
        {
            throw new NotImplementedException();
        }

        public TSelect Find<TSelect>(int id, Expression<Func<TEntity, TSelect>> @select, CrudOptions<TEntity> options)
        {
            throw new NotImplementedException();
        }

        public bool Update(int id, TEntity newValues, CrudOptions<TEntity> options)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id, CrudOptions<TEntity> options)
        {
            throw new NotImplementedException();
        }

        public Expression<Func<TEntity, bool>> CreateEqualityComparerExpression(int id)
            => x => x.Id.Equals(id);
        public Expression<Func<TEntity, bool>> CreateEqualityComparerExpression(IEnumerable<TEntity> others)
        {
            throw new NotImplementedException();
            return x => others.Any(o => o.Id == x.Id);
        }

        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }
    }
}