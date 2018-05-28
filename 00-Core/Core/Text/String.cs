using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using AMKsGear.Architecture.Data.Types;

namespace AMKsGear.Core.Text
{
    public static class StringUtil
    {
        #region Modifier Helpers
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string AppendText(this string str, string appendix)
        {
            return str + appendix;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string PrependText(this string str, string appendix)
        {
            return appendix + str;
        }

        public static IEnumerable<string> SplitAll(this IEnumerable<string> strings, params char[] separator)
        {
            if (strings == null) throw new ArgumentNullException(nameof(strings));
            return strings.Where(str => str != null).SelectMany(str => str.Split(separator));
        }
        #endregion

        #region TypeCast Helpers
        public static T ConvertTo<T>(this string str, Func<string, T> converter)
        {
            return converter(str);
        }
        
        public static bool ToBoolean(this string str, bool? defaultValue = null)
        {
            bool result;
            if (bool.TryParse(str, out result)) return result;
            if (defaultValue == null) throw new InvalidCastException();
            return defaultValue.Value;
        }
        public static short ToInt16(this string str, short? defaultValue = null)
        {
            short result;
            if (short.TryParse(str, out result)) return result;
            if (defaultValue == null) throw new InvalidCastException();
            return defaultValue.Value;
        }
        public static int ToInt32(this string str, int? defaultValue = null)
        {
            int result;
            if (int.TryParse(str, out result)) return result;
            if (defaultValue == null) throw new InvalidCastException();
            return defaultValue.Value;
        }
        public static long ToInt64(this string str, long? defaultValue = null)
        {
            long result;
            if (long.TryParse(str, out result)) return result;
            if (defaultValue == null) throw new InvalidCastException();
            return defaultValue.Value;
        }
        public static ushort ToUInt16(this string str, ushort? defaultValue = null)
        {
            ushort result;
            if (ushort.TryParse(str, out result)) return result;
            if (defaultValue == null) throw new InvalidCastException();
            return defaultValue.Value;
        }
        public static uint ToUInt32(this string str, uint? defaultValue = null)
        {
            uint result;
            if (uint.TryParse(str, out result)) return result;
            if (defaultValue == null) throw new InvalidCastException();
            return defaultValue.Value;
        }
        public static ulong ToUInt64(this string str, ulong? defaultValue = null)
        {
            ulong result;
            if (ulong.TryParse(str, out result)) return result;
            if (defaultValue == null) throw new InvalidCastException();
            return defaultValue.Value;
        }
        public static double ToDouble(this string str, double? defaultValue = null)
        {
            double result;
            if (double.TryParse(str, out result)) return result;
            if (defaultValue == null) throw new InvalidCastException();
            return defaultValue.Value;
        }
        public static float ToFloat(this string str, float? defaultValue = null)
        {
            float result;
            if (float.TryParse(str, out result)) return result;
            if (defaultValue == null) throw new InvalidCastException();
            return defaultValue.Value;
        }
        public static byte ToByte(this string str, byte? defaultValue = null)
        {
            byte result;
            if (byte.TryParse(str, out result)) return result;
            if (defaultValue == null) throw new InvalidCastException();
            return defaultValue.Value;
        }
        public static sbyte ToSByte(this string str, sbyte? defaultValue = null)
        {
            sbyte result;
            if (sbyte.TryParse(str, out result)) return result;
            if (defaultValue == null) throw new InvalidCastException();
            return defaultValue.Value;
        }
        public static char ToChar(this string str, char? defaultValue = null)
        {
            char result;
            if (char.TryParse(str, out result)) return result;
            if (defaultValue == null) throw new InvalidCastException();
            return defaultValue.Value;
        }
        public static decimal ToDecimal(this string str, decimal? defaultValue = null)
        {
            decimal result;
            if (decimal.TryParse(str, out result)) return result;
            if (defaultValue == null) throw new InvalidCastException();
            return defaultValue.Value;
        }
        public static DateTime ToDateTime(this string str, DateTime? defaultValue = null)
        {
            DateTime result;
            if (DateTime.TryParse(str, out result)) return result;
            if (defaultValue == null) throw new InvalidCastException();
            return defaultValue.Value;
        }
        public static DateTime ToDateTimeExact(this string str, string format, IFormatProvider formatProvider, DateTimeStyles styles, DateTime? defaultValue = null)
        {
            DateTime result;
            if (DateTime.TryParseExact(str, format, formatProvider, styles, out result)) return result;
            if (defaultValue == null) throw new InvalidCastException();
            return defaultValue.Value;
        }
        #endregion
        #region TypeCast Helpers (Nullable)
        public static bool? ToNullableBoolean(this string str, bool? defaultValue = null)
        {
            bool result;
            return bool.TryParse(str, out result) ? result : defaultValue;
        }
        public static short? ToNullableInt16(this string str, short? defaultValue = null)
        {
            short result;
            return short.TryParse(str, out result) ? result : defaultValue;
        }
        public static int? ToNullableInt32(this string str, int? defaultValue = null)
        {
            int result;
            return int.TryParse(str, out result) ? result : defaultValue;
        }
        public static long? ToNullableInt64(this string str, long? defaultValue = null)
        {
            long result;
            return long.TryParse(str, out result) ? result : defaultValue;
        }
        public static ushort? ToNullableUInt16(this string str, ushort? defaultValue = null)
        {
            ushort result;
            return ushort.TryParse(str, out result) ? result : defaultValue;
        }
        public static uint? ToNullableUInt32(this string str, uint? defaultValue = null)
        {
            uint result;
            return uint.TryParse(str, out result) ? result : defaultValue;
        }
        public static ulong? ToNullableUInt64(this string str, ulong? defaultValue = null)
        {
            ulong result;
            return ulong.TryParse(str, out result) ? result : defaultValue;
        }
        public static double? ToNullableDouble(this string str, double? defaultValue = null)
        {
            double result;
            return double.TryParse(str, out result) ? result : defaultValue;
        }
        public static float? ToNullableFloat(this string str, float? defaultValue = null)
        {
            float result;
            return float.TryParse(str, out result) ? result : defaultValue;
        }
        public static byte? ToNullableByte(this string str, byte? defaultValue = null)
        {
            byte result;
            return byte.TryParse(str, out result) ? result : defaultValue;
        }
        public static sbyte? ToNullableSByte(this string str, sbyte? defaultValue = null)
        {
            sbyte result;
            return sbyte.TryParse(str, out result) ? result : defaultValue;
        }
        public static char? ToNullableChar(this string str, char? defaultValue = null)
        {
            char result;
            return char.TryParse(str, out result) ? result : defaultValue;
        }
        public static decimal? ToNullableDecimal(this string str, decimal? defaultValue = null)
        {
            decimal result;
            return decimal.TryParse(str, out result) ? result : defaultValue;
        }
        public static DateTime? ToNullableDateTime(this string str, DateTime? defaultValue = null)
        {
            DateTime result;
            return (DateTime.TryParse(str, out result)) ? result : defaultValue;
        }
        public static DateTime? ToNullableDateTimeExact(this string str, string format, IFormatProvider formatProvider, DateTimeStyles styles, DateTime? defaultValue = null)
        {
            DateTime result;
            return (DateTime.TryParseExact(str, format, formatProvider, styles, out result)) ? result : defaultValue;
        }
        #endregion

        #region Comparer
        public static bool CompareString(this string source, string compareTo, StringCompare compare, StringComparison comparison)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (compareTo == null) throw new ArgumentNullException(nameof(compareTo));

            switch (compare)
            {
                case StringCompare.Equal:
                    return source.Equals(compareTo, comparison);
                case StringCompare.NotEqual:
                    return !source.Equals(compareTo, comparison);
                case StringCompare.Contains:
                    return source.IndexOf(compareTo, comparison) >= 0;
                case StringCompare.NotContains:
                    return source.IndexOf(compareTo, comparison) < 0;
                case StringCompare.RegexLike:
                    return Regex.IsMatch(source, compareTo);
                case StringCompare.StartsWith:
                    return source.StartsWith(compareTo, comparison);
                case StringCompare.NotStartsWith:
                    return !source.StartsWith(compareTo, comparison);
                case StringCompare.EndsWith:
                    return source.EndsWith(compareTo, comparison);
                case StringCompare.NotEndsWith:
                    return !source.EndsWith(compareTo, comparison);
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(compare), compare, null);
            }
        }

        #endregion
    }
}