using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using AMKsGear.Architecture.Data;
using AMKsGear.Architecture.Parallelism;
using AMKsGear.Core.Collections;
using AMKsGear.Core.Data;
using AMKsGear.Core.Parallelism;

namespace AMKsGear.Core.Automation.Mapper
{
    /// <summary>
    /// <see cref="Mapper"/>'s context to store all related data.
    /// </summary>
    /// <remarks>
    /// This class is thread-safe.
    /// </remarks>
    public class MapperContext : ICloneable, ICollection<Mapping>
    {
        protected IDictionary<int, Mapping> MappingRows { get; }
        protected ICacheContext<int, MappingCompiledInfo> CompiledCache { get; }


        protected ReaderWriterLockSlim Lock;
        public object CompileLockTarget { get; }

        private bool _isReadOnly = false;

        /// <summary>
        /// Determines the ability to add a mapping using general map options when mapping doesn't found.
        /// </summary>
        public bool AllowOnTheFlyMapping { get; internal set; }

        /// <summary>
        /// Determines if context already configured. (To prevent some single-set properties from being changed)
        /// </summary>
        public bool IsConfigured { get; internal set; }


        public MapperContext()
        {
            MappingRows = new Dictionary<int, Mapping>();
            CompiledCache = new CacheContext<int, MappingCompiledInfo>();

            Lock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
            CompileLockTarget = new object();
        }

        public MapperContext(IEnumerable<Mapping> mappingRows)
        {
            MappingRows = new Dictionary<int, Mapping>(mappingRows.ToDictionary(Mapping.ComputeHash));
            CompiledCache = new CacheContext<int, MappingCompiledInfo>();

            Lock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
            CompileLockTarget = new object();
        }

        protected MapperContext(IDictionary<int, Mapping> mappingRows)
        {
            MappingRows = new Dictionary<int, Mapping>(mappingRows);
            CompiledCache = new CacheContext<int, MappingCompiledInfo>();

            Lock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
            CompileLockTarget = new object();
        }


        /// <summary>
        /// A shallow copy of object.
        /// </summary>
        /// <returns>A new instance of <see cref="MapperContext"/> with shallow copied rows.</returns>
        public object Clone()
        {
            Lock.EnterReadLock();

            try
            {
                // Create a snapshot of list.
                return new MapperContext(MappingRows);
            }
            finally
            {
                Lock.ExitReadLock();
            }
        }


        /// <summary>
        /// Returns the <see cref="Mapping"/> using given destinationType and sourceType.
        /// </summary>
        /// <param name="destinationType"></param>
        /// <param name="sourceType"></param>
        /// <param name="mapping"></param>
        /// <returns>A value indicating that value is found or not.</returns>
        public bool TryGetMapping(Type destinationType, Type sourceType, out Mapping mapping)
        {
            var hash = Mapping.ComputeHash(destinationType, sourceType);

            Lock.EnterReadLock();

            try
            {
                return MappingRows.TryGetValue(hash, out mapping);
            }
            finally
            {
                Lock.ExitReadLock();
            }
        }

        /// <summary>
        /// Returns the <see cref="MappingCompiledInfo"/> using given destinationType and sourceType.
        /// </summary>
        /// <param name="destinationType"></param>
        /// <param name="sourceType"></param>
        /// <param name="mappingCompiledInfo"></param>
        /// <returns>A value indicating that value is found or not.</returns>
        public bool TryGetCompiledInfo(Type destinationType, Type sourceType,
            out MappingCompiledInfo mappingCompiledInfo)
        {
            var hash = Mapping.ComputeHash(destinationType, sourceType);

            Lock.EnterReadLock();

            try
            {
                return CompiledCache.TryGetValue(hash, out mappingCompiledInfo);
            }
            finally
            {
                Lock.ExitReadLock();
            }
        }

        public bool TryGetMappingAndCompiledInfo(Type destinationType, Type sourceType, out Mapping mapping,
            out MappingCompiledInfo mappingCompiledInfo)
        {
            var hash = Mapping.ComputeHash(destinationType, sourceType);

            Lock.EnterReadLock();

            try
            {
                var result = MappingRows.TryGetValue(hash, out mapping);
                CompiledCache.TryGetValue(hash, out mappingCompiledInfo);
                return result;
            }
            finally
            {
                Lock.ExitReadLock();
            }
        }

        public bool CacheCompiledInfo(Type destinationType, Type sourceType, MappingCompiledInfo mappingCompiledInfo)
        {
            var hash = Mapping.ComputeHash(destinationType, sourceType);

            Lock.EnterWriteLock();

            try
            {
                return CompiledCache.Cache(hash, mappingCompiledInfo);
            }
            finally
            {
                Lock.ExitWriteLock();
            }
        }

        protected internal int CacheCompiledInfos(IDictionary<int, MappingCompiledInfo> mappingCompiledInfos)
        {
            Lock.EnterWriteLock();

            try
            {
                return CompiledCache.CacheAll(mappingCompiledInfos);
            }
            finally
            {
                Lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Gets all mappings without equivalent compiled info. (Those which needing compilation).
        /// </summary>
        protected internal IDictionary<int, Mapping> GetMappingsWithoutCompiledInfo()
        {
            var result = new Dictionary<int, Mapping>();

            Lock.EnterReadLock();

            try
            {
                foreach (var row in MappingRows)
                {
                    if (!CompiledCache.Exists(row.Key))
                    {
                        result.Add(row.Key, row.Value);
                    }
                }

                return result;
            }
            finally
            {
                Lock.ExitReadLock();
            }
        }

        /// <summary>
        /// Merges another <see cref="MapperContext"/> with this instance of <see cref="MapperContext"/>.
        /// </summary>
        /// <remarks>
        /// This will modify current object.
        /// </remarks>
        /// <param name="mapperContext"></param>
        /// <param name="mergeBehavior"></param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void MergeWith(MapperContext mapperContext,
            MapperContextMergeBehavior mergeBehavior = MapperContextMergeBehavior.ThrowException)
        {
            switch (mergeBehavior)
            {
                case MapperContextMergeBehavior.OverwriteDuplicates:
                {
                    Lock.EnterWriteLock();

                    try
                    {
                        foreach (var row in mapperContext.MappingRows)
                            MappingRows[row.Key] = row.Value;
                    }
                    finally
                    {
                        Lock.ExitWriteLock();
                    }

                    break;
                }
                case MapperContextMergeBehavior.SkipDuplicates:
                {
                    Lock.EnterWriteLock();

                    try
                    {
                        foreach (var row in mapperContext.MappingRows)
                        {
                            if (!MappingRows.ContainsKey(row.Key))
                                MappingRows[row.Key] = row.Value;
                        }
                    }
                    finally
                    {
                        Lock.ExitWriteLock();
                    }

                    break;
                }
                case MapperContextMergeBehavior.ThrowException:
                {
                    Lock.EnterReadLock();

                    try
                    {
                        foreach (var row in mapperContext.MappingRows)
                        {
                            if (MappingRows.ContainsKey(row.Key))
                            {
                                throw new InvalidOperationException();
                            }
                        }
                    }
                    finally
                    {
                        Lock.ExitReadLock();
                    }

                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(mergeBehavior), mergeBehavior, null);
            }
        }


        /// <summary>
        /// Returns an enumerator to a snapshot of mappings.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Mapping> GetEnumerator()
        {
            Lock.EnterReadLock();

            try
            {
                // Create a snapshot of list.
                return MappingRows.Values
                    .GetEnumerator();
            }
            finally
            {
                Lock.ExitReadLock();
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


        /// <summary>
        /// Adds a mapping to <see cref="MapperContext"/>.
        /// </summary>
        /// <param name="item"></param>
        public void Add(Mapping item)
        {
            var hash = Mapping.ComputeHash(item);

            Lock.EnterWriteLock();

            try
            {
                MappingRows.Add(hash, item);
                CompiledCache.Miss(hash);
            }
            finally
            {
                Lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Adds a range of mappings to <see cref="MapperContext"/>.
        /// </summary>
        /// <param name="items"></param>
        public void AddRange(IEnumerable<Mapping> items)
        {
            var hashes = new List<int>();

            Lock.EnterWriteLock();

            try
            {
                foreach (var item in items)
                {
                    var hash = Mapping.ComputeHash(item);
                    MappingRows.Add(hash, item);
                    hashes.Add(hash);
                }

                CompiledCache.MissAll(hashes);
            }
            finally
            {
                Lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Clears all mappings and compiled caches.
        /// </summary>
        public void Clear()
        {
            Lock.EnterWriteLock();

            try
            {
                MappingRows.Clear();
                CompiledCache.Clear();
            }
            finally
            {
                Lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Determines if a mapping exists in this <see cref="MapperContext"/>.
        /// </summary>
        /// <param name="destinationType"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public bool Contains(Type destinationType, Type sourceType)
        {
            var hash = Mapping.ComputeHash(destinationType, sourceType);

            Lock.EnterReadLock();

            try
            {
                return MappingRows.ContainsKey(hash);
            }
            finally
            {
                Lock.ExitReadLock();
            }
        }

        /// <summary>
        /// Determines if a mapping exists in this <see cref="MapperContext"/>.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(Mapping item)
        {
            var hash = Mapping.ComputeHash(item);

            Lock.EnterReadLock();

            try
            {
                return MappingRows.ContainsKey(hash);
            }
            finally
            {
                Lock.ExitReadLock();
            }
        }

        /// <summary>
        /// Copies all mappings to an array.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(Mapping[] array, int arrayIndex)
        {
            Lock.EnterReadLock();

            try
            {
                MappingRows.Values.CopyTo(array, arrayIndex);
            }
            finally
            {
                Lock.ExitReadLock();
            }
        }

        /// <summary>
        /// Removes a mapping (and it's compiled cache) from <see cref="MapperContext"/>.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(Mapping item)
        {
            var hash = Mapping.ComputeHash(item);

            Lock.EnterWriteLock();

            try
            {
                var result = MappingRows.Remove(hash);
                CompiledCache.Miss(hash);
                return result;
            }
            finally
            {
                Lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Removes a mapping (and it's compiled cache) from <see cref="MapperContext"/>.
        /// </summary>
        /// <param name="destinationType"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public bool Remove(Type destinationType, Type sourceType)
        {
            var hash = Mapping.ComputeHash(destinationType, sourceType);

            Lock.EnterWriteLock();

            try
            {
                var result = MappingRows.Remove(hash);
                CompiledCache.Miss(hash);
                return result;
            }
            finally
            {
                Lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Returns the number of maps in this instance of <see cref="MapperContext"/>.
        /// </summary>
        public int Count
        {
            get
            {
                Lock.EnterReadLock();

                try
                {
                    return MappingRows.Count;
                }
                finally
                {
                    Lock.ExitReadLock();
                }
            }
        }

        public bool IsReadOnly => _isReadOnly;
    }
}