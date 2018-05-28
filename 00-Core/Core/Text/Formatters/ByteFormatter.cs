using System;
using System.Collections.Generic;

namespace AMKsGear.Core.Text.Formatters
{
    public enum ByteFormatterCalculationMode
    {
        Binary,
        Decimal,
        BinaryCalcDecimalSign
    }

    public class ByteFormatter : ICustomFormatter, IFormatProvider
    {
        private const long KiB = 1024;
        private const long MiB = KiB * 1024;
        private const long GiB = MiB * 1024;

        private const long KB = 1000;
        private const long MB = KB * 1000;
        private const long GB = MB * 1000;

        private static readonly Dictionary<int, string> FormatPatternsDecimal = new Dictionary<int, string>()
        {
            {0, "{0} B"},
            {1, "{0:0[PERCICION]} KB"},
            {2, "{0:0[PERCICION]} MB"},
            {3, "{0:0[PERCICION]} GB"},
            {4, "{0:0[PERCICION]} TB"},
            {5, "{0:0[PERCICION]} PB"},
            {6, "{0:0[PERCICION]} EB"},
            {7, "{0:0[PERCICION]} ZB"},
            {8, "{0:0[PERCICION]} YB"},
        };

        private static readonly Dictionary<int, string> FormatPatternsBinary = new Dictionary<int, string>()
        {
            {0, "{0} B"},
            {1, "{0:0[PERCICION]} KiB"},
            {2, "{0:0[PERCICION]} MiB"},
            {3, "{0:0[PERCICION]} GiB"},
            {4, "{0:0[PERCICION]} TiB"},
            {5, "{0:0[PERCICION]} PiB"},
            {6, "{0:0[PERCICION]} EiB"},
            {7, "{0:0[PERCICION]} ZiB"},
            {8, "{0:0[PERCICION]} YiB"},
        };

        public ByteFormatterCalculationMode CalculationMode { get; }

        private readonly int _shift = 0;
        private readonly int _percicion = 0;

        public ByteFormatter(ByteFormatterCalculationMode calculationMode = ByteFormatterCalculationMode.Binary)
        {
            CalculationMode = calculationMode;
        }

        public ByteFormatter(int percicion,
            ByteFormatterCalculationMode calculationMode = ByteFormatterCalculationMode.Binary)
        {
            CalculationMode = calculationMode;
            _percicion = percicion;
        }

        public ByteFormatter(int percicion, int shift,
            ByteFormatterCalculationMode calculationMode = ByteFormatterCalculationMode.Binary)
        {
            CalculationMode = calculationMode;
            _percicion = percicion;
            _shift = shift;
        }

        public string ToString(byte size) => ToString((long) size);
        public string ToString(char size) => ToString((long) size);
        public string ToString(int size) => ToString((long) size);
        public string ToString(short size) => ToString((long) size);
        public string ToString(long size)
        {
            long kb;
            Dictionary<int, string> presentation;
            switch (CalculationMode)
            {
                case ByteFormatterCalculationMode.Binary:
                    kb = KiB;
                    presentation = FormatPatternsBinary;
                    break;
                case ByteFormatterCalculationMode.Decimal:
                    kb = KB;
                    presentation = FormatPatternsDecimal;
                    break;
                case ByteFormatterCalculationMode.BinaryCalcDecimalSign:
                    kb = KiB;
                    presentation = FormatPatternsDecimal;
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }
            
            if (size < kb)
            {
                return string.Format(presentation[_shift], size);
            }
            else
            {
                int thousands = 0;
                double modulus = size;
                //double percicion = 0;
                while (modulus > kb)
                {
                    modulus = modulus / kb;
                    //percicion = (percicion + (modulus % KB)) * 0.001d;
                    ++thousands;
                }

                return string.Format(presentation[_shift + thousands]
                    .Replace("[PERCICION]", _percicion > 0 ? '.' + new string('0', _percicion) : null), modulus);
            }
        }

        public string ToString(uint size) => ToString((ulong) size);
        public string ToString(ushort size) => ToString((ulong) size);
        public string ToString(ulong size)
        {
            ulong kb;
            Dictionary<int, string> presentation;
            switch (CalculationMode)
            {
                case ByteFormatterCalculationMode.Binary:
                    kb = KiB;
                    presentation = FormatPatternsBinary;
                    break;
                case ByteFormatterCalculationMode.Decimal:
                    kb = KB;
                    presentation = FormatPatternsDecimal;
                    break;
                case ByteFormatterCalculationMode.BinaryCalcDecimalSign:
                    kb = KiB;
                    presentation = FormatPatternsDecimal;
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }
            
            if (size < kb)
            {
                return string.Format(presentation[_shift], size);
            }
            else
            {
                int thousands = 0;
                double modulus = size;
                //double percicion = 0;
                while (modulus > kb)
                {
                    modulus = modulus / kb;
                    //percicion = (percicion + (modulus % KB)) * 0.001d;
                    ++thousands;
                }

                return string.Format(presentation[_shift + thousands]
                    .Replace("[PERCICION]", _percicion > 0 ? '.' + new string('0', _percicion) : null), modulus);
            }
        }

        public string ToString(float size) => ToString((double) size);
        public string ToString(double size)
        {
            long kb;
            Dictionary<int, string> presentation;
            switch (CalculationMode)
            {
                case ByteFormatterCalculationMode.Binary:
                    kb = KiB;
                    presentation = FormatPatternsBinary;
                    break;
                case ByteFormatterCalculationMode.Decimal:
                    kb = KB;
                    presentation = FormatPatternsDecimal;
                    break;
                case ByteFormatterCalculationMode.BinaryCalcDecimalSign:
                    kb = KiB;
                    presentation = FormatPatternsDecimal;
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }
            
            if (size < kb)
            {
                return string.Format(presentation[_shift], size);
            }
            else
            {
                int thousands = 0;
                double modulus = size;
                while (modulus > kb)
                {
                    modulus = modulus / kb;
                    ++thousands;
                }

                return string.Format(presentation[_shift + thousands], modulus);
            }
        }

        public string ToString(decimal size)
        {
            long kb;
            Dictionary<int, string> presentation;
            switch (CalculationMode)
            {
                case ByteFormatterCalculationMode.Binary:
                    kb = KiB;
                    presentation = FormatPatternsBinary;
                    break;
                case ByteFormatterCalculationMode.Decimal:
                    kb = KB;
                    presentation = FormatPatternsDecimal;
                    break;
                case ByteFormatterCalculationMode.BinaryCalcDecimalSign:
                    kb = KiB;
                    presentation = FormatPatternsDecimal;
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }
            
            if (size < kb)
            {
                return string.Format(presentation[_shift], size);
            }
            else
            {
                int thousands = 0;
                decimal modulus = size;
                while (modulus > kb)
                {
                    modulus = modulus / kb;
                    ++thousands;
                }

                return string.Format(presentation[_shift + thousands], modulus);
            }
        }

        #region ICustomFormatter implementation

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            decimal val;
            if (arg is int)
            {
                return ToString((int) arg);
            }
            else if (arg is uint)
            {
                return ToString((uint) arg);
            }
            else if (arg is long)
            {
                return ToString((long) arg);
            }
            else if (arg is ulong)
            {
                return ToString((ulong) arg);
            }
            else if (arg is double)
            {
                return ToString((double) arg);
            }
            else if (arg is float)
            {
                return ToString((float) arg);
            }
            else if (arg is decimal)
            {
                return ToString((decimal) arg);
            }
            else if (arg is string && decimal.TryParse(arg as string, out val))
            {
                return ToString(val);
            }
            else if (arg is short)
            {
                return ToString((short) arg);
            }
            else if (arg is ushort)
            {
                return ToString((ushort) arg);
            }
            else if (arg is byte)
            {
                return ToString((byte) arg);
            }
            else if (arg is char)
            {
                return ToString((char) arg);
            }
            else
            {
                return arg?.ToString();
            }
        }

        #endregion

        #region IFormatProvider implementation

        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }
            return null;
        }

        #endregion
    }
}