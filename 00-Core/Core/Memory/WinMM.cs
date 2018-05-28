//using System;
//using System.Runtime.InteropServices;

//namespace AMKsGear.Core.Memory
//{
//    internal unsafe class WinMM
//    {
//        #region API Imports
//        [DllImport("kernel32", SetLastError = false)]
//        public static extern IntPtr RtlMoveMemory(IntPtr dest, IntPtr src, int count);
//        [DllImport("kernel32", SetLastError = false)]
//        public static extern IntPtr RtlMoveMemory(IntPtr dest, int* src, int count);
//        [DllImport("kernel32", SetLastError = false)]
//        public static extern IntPtr RtlMoveMemory(int* dest, IntPtr src, int count);
//        [DllImport("kernel32", SetLastError = false)]
//        public static extern IntPtr RtlMoveMemory(int* dest, int* src, int count);
//        [DllImport("kernel32", SetLastError = false)]
//        public static extern IntPtr RtlMoveMemory(byte* dest, byte* src, int count);

//        [DllImport("kernel32", SetLastError = false)]
//        public static extern IntPtr RtlCopyMemory(IntPtr dest, IntPtr src, int count);
//        [DllImport("kernel32", SetLastError = false)]
//        public static extern IntPtr RtlCopyMemory(IntPtr dest, int* src, int count);
//        [DllImport("kernel32", SetLastError = false)]
//        public static extern IntPtr RtlCopyMemory(int* dest, IntPtr src, int count);
//        [DllImport("kernel32", SetLastError = false)]
//        public static extern IntPtr RtlCopyMemory(int* dest, int* src, int count);
//        [DllImport("kernel32", SetLastError = false)]
//        public static extern IntPtr RtlCopyMemory(byte* dest, byte* src, int count);


//        public const int HEAP_ZERO_MEMORY = 0x00000008;
//        [DllImport("kernel32")]
//        public static extern IntPtr GetProcessHeap();
//        [DllImport("kernel32")]
//        public static extern IntPtr HeapAlloc(IntPtr hHeap, int flags, int size);
//        [DllImport("kernel32")]
//        public static extern bool HeapFree(IntPtr hHeap, int flags, IntPtr block);
//        [DllImport("kernel32")]
//        public static extern IntPtr HeapReAlloc(IntPtr hHeap, int flags, IntPtr block, int size);
//        [DllImport("kernel32")]
//        public static extern int HeapSize(IntPtr hHeap, int flags, IntPtr block);
//        #endregion
//    }
//}