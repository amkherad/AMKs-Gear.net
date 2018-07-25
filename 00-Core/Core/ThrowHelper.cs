using System;
using System.Runtime.CompilerServices;

namespace AMKsGear.Core
{
    public class ThrowHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowIfNull_Arg(object obj, string name)
        {
            if (ReferenceEquals(obj, null))
            {
                throw new ArgumentNullException(name);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OutOfRange_Enum(object val, string varName)
        {
            ThrowIfNull_Arg(val, nameof(val));

            throw new IndexOutOfRangeException($"Index was out of range of enum members. value: {val} , var: {varName}");
        }
    }
}