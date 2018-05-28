//using System;

//namespace AMKsGear.Core.Memory
//{
//    public unsafe class Heap
//    {
//        #region Static Region
//        public static readonly IntPtr ProcessHeap = WinMM.GetProcessHeap();
        
//        public static IntPtr Alloc(int size)
//        {
//            if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
//            var result = WinMM.HeapAlloc(ProcessHeap, WinMM.HEAP_ZERO_MEMORY, size);
//            if (result == IntPtr.Zero) throw new OutOfMemoryException();
//            return result;
//        }

//        public static void Free(IntPtr block)
//        {
//            if (!WinMM.HeapFree(ProcessHeap, 0, block))
//                throw new InvalidOperationException();
//        }
//        // Re-allocates a memory block. If the reallocation request is for a
//        // larger size, the additional region of memory is automatically
//        // initialized to zero.
//        public static IntPtr ReAlloc(IntPtr block, int size)
//        {
//            if (size < 0) throw new ArgumentOutOfRangeException(nameof(size));
//            var result = WinMM.HeapReAlloc(ProcessHeap, WinMM.HEAP_ZERO_MEMORY, block, size);
//            if (result == IntPtr.Zero) throw new OutOfMemoryException();
//            return result;
//        }
//        // Returns the size of a memory block.
//        public static int SizeOf(IntPtr block)
//        {
//            int result = WinMM.HeapSize(ProcessHeap, 0, block);
//            if (result == -1) throw new InvalidOperationException();
//            return result;
//        }
//        #endregion
//    }
//}