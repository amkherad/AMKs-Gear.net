using System.Collections.Generic;
using AMKsGear.Architecture;
using AMKsGear.Architecture.Data;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMemberInSuper.Global

namespace AMKsGear.Core.Data.AbstractInterface
{
    public interface ICrudServiceOptions
    {
        CacheMode CacheMode { get; }
        object Context { get; }
    }
    public interface ICrudServiceOptionsAutomationSupport : ICrudServiceOptions
    {
        void SetCacheMode(CacheMode cacheMode);
        void SetContext(object context);
    }

    public interface IPagableCrudServiceOptions<TEntity> : ICrudServiceOptions
    {
        long? Skip { get; }
        long? Take { get; }

        IEnumerable<SortingContext<TEntity>> Sorting { get; }
    }
    public interface IPagableCrudServiceOptionsAutomationSupport<TEntity>
        : IPagableCrudServiceOptions<TEntity>, ICrudServiceOptionsAutomationSupport
    {
        void SetSkip(long value);
        void SetTake(long value);

        void SetSorting(IEnumerable<SortingContext<TEntity>> sorting);
    }
}