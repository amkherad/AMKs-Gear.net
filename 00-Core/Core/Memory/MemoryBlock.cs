//using System;

//namespace AMKsGear.Core.Memory
//{
//    public unsafe class MemoryBlock : IMemoryBlock
//    {
//        #region Class Implementations
//        private volatile IntPtr _ref;
//        private volatile bool _immutable = false;

//        public MemoryBlock(int size)
//        {
//            GC.AddMemoryPressure(size);
//            _ref = Heap.Alloc(size);
//        }
//        public MemoryBlock(int size, bool immutable)
//        {
//            GC.AddMemoryPressure(size);
//            _ref = Heap.Alloc(size);
//            _immutable = immutable;
//        }
//        ~MemoryBlock() { Dispose(); }

//        public void Dispose()
//        {
//            var @ref = _ref;
//            if (IsDisposed) return;
//            _ref = IntPtr.Zero;
//            GC.RemoveMemoryPressure(Heap.SizeOf(@ref));
//            Heap.Free(@ref);
//            GC.SuppressFinalize(this);
//        }

//        public IntPtr Reference
//        {
//            get
//            {
//                if (IsDisposed) throw new ObjectDisposedException(nameof(MemoryBlock));
//                return _ref;
//            }
//        }

//        public bool IsDisposed => _ref == IntPtr.Zero;

//        public int Size
//        {
//            get
//            {
//                if (IsDisposed) throw new ObjectDisposedException(nameof(MemoryBlock));
//                return Heap.SizeOf(_ref);
//            }
//        }

//        public bool IsImmutable
//        {
//            get
//            {
//                if (IsDisposed) throw new ObjectDisposedException(nameof(MemoryBlock));
//                return _immutable;
//            }
//        }

//        public static implicit operator IntPtr(MemoryBlock mb) { return mb.Reference; }
//        public static implicit operator void* (MemoryBlock mb) { return mb.Reference.ToPointer(); }

//        public byte[] ToArray()
//        {
//            if (IsDisposed) throw new ObjectDisposedException(nameof(MemoryBlock));
//            var size = Size;
//            var retVal = new byte[size];
//            fixed (byte* bPtr = retVal)
//                MemoryManager.Copy(new IntPtr(bPtr), _ref, size);
//            return retVal;
//        }

//        public object Clone()
//        {
//            if (IsDisposed) throw new ObjectDisposedException(nameof(MemoryBlock));
//            var size = Size;
//            var mb = new MemoryBlock(size);
//            MemoryManager.Copy(_ref, mb._ref, size);
//            return mb;
//        }

//        public static void Resize(MemoryBlock mb, int size)
//        {
//            if (mb == null) throw new ArgumentNullException(nameof(mb));
//            if (mb.IsImmutable) throw new InvalidOperationException($"{nameof(MemoryBlock)} is immutable.");
//            if (mb.IsDisposed) throw new ObjectDisposedException(nameof(mb));

//            var _ref = mb.Reference;
//            if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
//            mb._ref = _ref == IntPtr.Zero ? Heap.Alloc(size) : Heap.ReAlloc(_ref, size);
//        }
//        #endregion
//    }
//}