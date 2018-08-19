using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using AMKsGear.Architecture.Annotations;

namespace AMKsGear.Core.Linq.Convert
{
    public static class TypeConvertHelper
    {
        /// <summary>
        /// <see cref="Convert"/> conversion method mapping to it's type.
        /// </summary>
        public static readonly ReadOnlyDictionary<Type, string> ConvertMethodNameMapping =
            new ReadOnlyDictionary<Type, string>(
                new Dictionary<Type, string>(15)
                {
                    {typeof(int), nameof(System.Convert.ToInt32)},
                    {typeof(long), nameof(System.Convert.ToInt64)},
                    {typeof(bool), nameof(System.Convert.ToBoolean)},
                    {typeof(double), nameof(System.Convert.ToDouble)},
                    {typeof(float), nameof(System.Convert.ToSingle)},
                    {typeof(byte), nameof(System.Convert.ToByte)},
                    {typeof(char), nameof(System.Convert.ToChar)},
                    {typeof(short), nameof(System.Convert.ToInt16)},
                    {typeof(decimal), nameof(System.Convert.ToDecimal)},
                    {typeof(uint), nameof(System.Convert.ToUInt32)},
                    {typeof(ulong), nameof(System.Convert.ToUInt64)},
                    {typeof(ushort), nameof(System.Convert.ToUInt16)},
                    {typeof(sbyte), nameof(System.Convert.ToSByte)},
                    {typeof(DateTime), nameof(System.Convert.ToDateTime)}
                });


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