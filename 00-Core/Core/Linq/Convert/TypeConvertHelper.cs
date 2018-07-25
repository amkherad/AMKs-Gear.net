using System;
using System.Runtime.CompilerServices;
using AMKsGear.Architecture.Annotations;

namespace AMKsGear.Core.Linq.Convert
{
    public static class TypeConvertHelper
    {
        //@LocalizationRequired
        [LocalizationRequired]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TypeConvertException ConvertException(Type from, Type to)
        {
            return new TypeConvertException();
        }
        
        //@LocalizationRequired
        [LocalizationRequired]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TypeConvertException ConvertException(Type from)
        {
            return new TypeConvertException();
        }
    }
}