using System;
using System.Runtime.CompilerServices;

namespace AMKsGear.Core
{
    public class ThrowHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowIfNull_Arg(object obj, string name)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(name);
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowIfNull_Args(object obj1, string name1, object obj2, string name2)
        {
            if (obj1 == null)
            {
                throw new ArgumentNullException(name1);
            }
            if (obj2 == null)
            {
                throw new ArgumentNullException(name2);
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowIfNull_Args(object obj1, string name1, object obj2, string name2, object obj3, string name3)
        {
            if (obj1 == null)
            {
                throw new ArgumentNullException(name1);
            }
            if (obj2 == null)
            {
                throw new ArgumentNullException(name2);
            }
            if (obj3 == null)
            {
                throw new ArgumentNullException(name3);
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