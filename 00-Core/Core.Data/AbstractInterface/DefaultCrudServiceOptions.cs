using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AMKsGear.Architecture;
using AMKsGear.Architecture.Data;
using AMKsGear.Core.Automation.IoC;

namespace AMKsGear.Core.Data.AbstractInterface
{
    public class DefaultCrudServiceOptions : ICrudServiceOptionsAutomationSupport
    {
        public const long DefaultWidePageSize = 10;

        public CacheMode CacheMode { get; private set; }
        public object Context { get; private set; }

        public void SetCacheMode(CacheMode cacheMode) => CacheMode = cacheMode;
        public void SetContext(object context) => Context = context;
        
        public static ICrudServiceOptions FromContextDefault(object context) => new DefaultCrudServiceOptions { Context = context };
        public static ICrudServiceOptions CachedDefault(object context = null) => new DefaultCrudServiceOptions { Context = context, CacheMode = CacheMode.Cache };

        public static TOptions Default<TOptions>() => default(TOptions);

        public static TOptions FromContext<TOptions>(object context)
            where TOptions : ICrudServiceOptions
        {
            var instance = TypeResolver.CreateInstance<TOptions>() as ICrudServiceOptionsAutomationSupport;
            if (instance == null) throw new InvalidOperationException();
            
            instance.SetContext(context);

            return (TOptions)instance;
        }
        public static TOptions Cached<TOptions>(CacheMode cacheMode)
            where TOptions : ICrudServiceOptionsAutomationSupport
        {
            var instance = TypeResolver.CreateInstance<TOptions>();
            if (instance == null) throw new InvalidOperationException();

            instance.SetCacheMode(cacheMode);

            return instance;
        }
        public static TOptions FromPagingParameters<TEntity, TOptions>(
            Expression<Func<TEntity, object>> sortField, SortingOrder sortingOrder,
            long skip, long take)
            where TOptions : IPagableCrudServiceOptionsAutomationSupport<TEntity>
        {
            var instance = TypeResolver.CreateInstance<TOptions>();
            if (instance == null) throw new InvalidOperationException();

            if (sortField != null) instance.SetSorting(new[] {new SortingContext<TEntity>(sortField, sortingOrder)});
            instance.SetSkip(skip);
            instance.SetTake(take);

            return instance;
        }
    }
    public class DefaultCrudServiceOptions<TEntity> : DefaultCrudServiceOptions, IPagableCrudServiceOptionsAutomationSupport<TEntity>
    {
        public long? Skip { get; private set; }
        public long? Take { get; private set; }
        public IEnumerable<SortingContext<TEntity>> Sorting { get; private set; }

        public void SetSkip(long value) => Skip = value;
        public void SetTake(long value) => Take = value;
        public void SetSorting(IEnumerable<SortingContext<TEntity>> sorting) => Sorting = sorting;
    }
}
