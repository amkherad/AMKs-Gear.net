using System;
using System.Text;

namespace AMKsGear.Core.Automation
{
    public static class ByteExtensions
    {
        public static bool CompareWithAnother(this byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;
            
//            if (Environment.Is64BitProcess)
//            {
//                var length = System.Math.DivRem(a.Length, 8, out var reminder);
//                int i = 0;
//                Span<long> pointerA = a.AsSpan();
//                Span<long> pointerB = b.AsSpan();
//                for (; i < length; i++, pointerA++)
//                {
//                    if (pointerA[i] != pointerB[i]) return false;
//                }
//            }
//            else
//            {
//                
//            }
//            
            for (int i = 0; i < a.Length; i++)
                if (a[i] != b[i])
                    return false;
            return true;
        }
    }
}