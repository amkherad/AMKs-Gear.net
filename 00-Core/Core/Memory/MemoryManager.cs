//using System;

//namespace AMKsGear.Core.Memory
//{
//    public static unsafe class MemoryManager
//    {
//        /// <summary>
//        /// Copies count bytes from src to dst. The source and destination
//        //  blocks are permitted to overlap.
//        //  Frees a memory block.
//        /// </summary>
//        public static void Copy(IntPtr destination, IntPtr source, int length) => WinMM.RtlCopyMemory(destination, source, length);
//        public static void Copy(int* destination, int* source, int length) => WinMM.RtlCopyMemory(destination, source, length);
//        public static void Copy(byte* destination, byte* source, int length) => WinMM.RtlCopyMemory(destination, source, length);
//        public static void Copy(IntPtr destination, int* source, int length) => WinMM.RtlCopyMemory(destination, source, length);
//        public static void Copy(int* destination, IntPtr source, int length) => WinMM.RtlCopyMemory(destination, source, length);
//        public static void Copy(IMemoryBlock destination, IMemoryBlock source)
//        {
//            if (source == null) throw new ArgumentNullException(nameof(source));
//            if (destination == null) throw new ArgumentNullException(nameof(destination));
//            var srcSize = source.Size;
//            if (srcSize != destination.Size) throw new InvalidOperationException();
//            WinMM.RtlCopyMemory(destination.Reference, source.Reference, srcSize);
//        }

//        public static IMemoryBlock Allocate(int size) => new MemoryBlock(size);
//    }
//}