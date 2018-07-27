using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AMKsGear.Architecture.Parallelism;
using AMKsGear.Core.Parallelism;

namespace AMKsGear.Core.Automation.Mapper
{
    /// <summary>
    /// <see cref="Mapper"/>'s context to store all related data.
    /// </summary>
    public class MapperContext : ICloneable, ICollection<MappingRow>
    {
        protected internal HashSet<MappingRow> MappingRows { get; }
        protected internal ReaderWriterLockSlim Lock;
        private object _compileLock;

        private bool _isReadOnly = false;

        
        public MapperContext()
        {
            MappingRows = new HashSet<MappingRow>();
            Lock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
            _compileLock = new object();
        }

        public MapperContext(IEnumerable<MappingRow> trackerRows)
        {
            MappingRows = new HashSet<MappingRow>(trackerRows);
            Lock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
            _compileLock = new object();
        }


        public ISyncBlock CompileLock() => new MonitorBlock(_compileLock);


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


        public bool TryGetMapping(Type destination, Type source, out MappingRow mappingRow)
        {
            Lock.EnterReadLock();

            try
            {
                return MappingRows.TryGetValue(new MappingRow(destination, source), out mappingRow);
            }
            finally
            {
                Lock.ExitReadLock();
            }
        }


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
                        MappingRows.ExceptWith(mapperContext.MappingRows);
                        MappingRows.UnionWith(mapperContext.MappingRows);
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
                        MappingRows.UnionWith(mapperContext.MappingRows);
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
                        if (MappingRows.Overlaps(mapperContext.MappingRows))
                        {
                            throw new InvalidOperationException();
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

        
        public IEnumerator<MappingRow> GetEnumerator()
        {
            Lock.EnterReadLock();

            try
            {
                // Create a snapshot of list.
                return MappingRows.ToList()
                    .GetEnumerator();
            }
            finally
            {
                Lock.ExitReadLock();
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


        public void Add(MappingRow item)
        {
            Lock.EnterWriteLock();

            try
            {
                MappingRows.Add(item);
            }
            finally
            {
                Lock.ExitWriteLock();
            }
        }

        public void AddRange(IEnumerable<MappingRow> items)
        {
            Lock.EnterWriteLock();

            try
            {
                foreach (var item in items)
                    MappingRows.Add(item);
            }
            finally
            {
                Lock.ExitWriteLock();
            }
        }

        public void Clear()
        {
            Lock.EnterWriteLock();

            try
            {
                MappingRows.Clear();
            }
            finally
            {
                Lock.ExitWriteLock();
            }
        }

        public bool Contains(Type destinationType, Type sourceType)
        {
            Lock.EnterReadLock();

            try
            {
                return MappingRows.Contains(new MappingRow(destinationType, sourceType));
            }
            finally
            {
                Lock.ExitReadLock();
            }
        }

        public bool Contains(MappingRow item)
        {
            Lock.EnterReadLock();

            try
            {
                return MappingRows.Contains(item);
            }
            finally
            {
                Lock.ExitReadLock();
            }
        }

        public void CopyTo(MappingRow[] array, int arrayIndex)
        {
            Lock.EnterReadLock();

            try
            {
                MappingRows.CopyTo(array, arrayIndex);
            }
            finally
            {
                Lock.ExitReadLock();
            }
        }

        public bool Remove(MappingRow item)
        {
            Lock.EnterWriteLock();

            try
            {
                return MappingRows.Remove(item);
            }
            finally
            {
                Lock.ExitWriteLock();
            }
        }

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