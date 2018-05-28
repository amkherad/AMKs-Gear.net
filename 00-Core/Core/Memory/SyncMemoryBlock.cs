//using System;

//namespace AMKsGear.Core.Memory
//{
//    public unsafe class SyncMemoryBlock : IMemoryBlock
//    {
//        #region Class Implementations

//        private volatile IntPtr _ref;
//        private readonly object _refLock = new object();
//        private volatile bool _immutable = false;

//        public SyncMemoryBlock(int size)
//        {
//            lock (this)
//            {
//                GC.AddMemoryPressure(size);
//                _ref = Heap.Alloc(size);
//            }
//        }
//        public SyncMemoryBlock(int size, bool immutable)
//        {
//            lock (this)
//            {
//                GC.AddMemoryPressure(size);
//                _ref = Heap.Alloc(size);
//                _immutable = immutable;
//            }
//        }

//        ~SyncMemoryBlock() { Dispose(); }

//        public void Dispose()
//        {
//            if (IsDisposed) return;
//            lock (this)
//            {
//                if (IsDisposed) return;
//                lock (_refLock)
//                {
//                    GC.RemoveMemoryPressure(Heap.SizeOf(_ref));
//                    Heap.Free(_ref);
//                    _ref = IntPtr.Zero;
//                }
//            }
//        }

//        public IntPtr Reference
//        {
//            get
//            {
//                lock (this)
//                {
//                    if (IsDisposed) throw new ObjectDisposedException(nameof(SyncMemoryBlock));
//                    return _ref;
//                }
//            }
//        }

//        public bool IsDisposed
//        {
//            get
//            {
//                lock (this)
//                    lock (_refLock)
//                        return _ref == IntPtr.Zero;
//            }
//        }

//        public int Size
//        {
//            get
//            {
//                lock (this)
//                {
//                    if (IsDisposed) throw new ObjectDisposedException(nameof(SyncMemoryBlock));
//                    return Heap.SizeOf(_ref);
//                }
//            }
//        }

//        public bool IsImmutable
//        {
//            get
//            {
//                lock (this)
//                {
//                    if (IsDisposed) throw new ObjectDisposedException(nameof(SyncMemoryBlock));
//                    return _immutable;
//                }
//            }
//        }

//        public static implicit operator IntPtr(SyncMemoryBlock mb) { return mb.Reference; }
//        public static implicit operator void*(SyncMemoryBlock mb) { return mb.Reference.ToPointer(); }

//        public byte[] ToArray()
//        {
//            lock (this)
//            {
//                if (IsDisposed) throw new ObjectDisposedException(nameof(SyncMemoryBlock));
//                var size = Size;
//                var retVal = new byte[size];
//                fixed (byte* bPtr = retVal)
//                    MemoryManager.Copy(new IntPtr(bPtr), _ref, size);
//                return retVal;
//            }
//        }

//        public object Clone()
//        {
//            lock (this)
//            {
//                if (IsDisposed) throw new ObjectDisposedException(nameof(SyncMemoryBlock));
//                var size = Size;
//                var mb = new SyncMemoryBlock(size);
//                MemoryManager.Copy(_ref, mb._ref, size);
//                return mb;
//            }
//        }

//        public static void Resize(SyncMemoryBlock mb, int size)
//        {
//            if (mb == null) throw new ArgumentNullException(nameof(mb));
//            if (mb.IsImmutable) throw new InvalidOperationException($"{nameof(SyncMemoryBlock)} is immutable.");
//            if (mb.IsDisposed) throw new ObjectDisposedException(nameof(mb));

//            var _ref = mb.Reference;
//            if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
//            lock (mb._refLock)
//                mb._ref = _ref == IntPtr.Zero ? Heap.Alloc(size) : Heap.ReAlloc(_ref, size);
//        }
//        #endregion
//    }
//}