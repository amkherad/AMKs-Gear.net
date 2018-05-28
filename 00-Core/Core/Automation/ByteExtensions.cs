using System;
using System.Text;

namespace AMKsGear.Core.Automation
{
    public static class ByteExtensions
    {
        public static bool CompareWithAnother(this byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;
            for (int i = 0; i < a.Length; i++)
                if (a[i] != b[i])
                    return false;
            return true;
        }

        public static string GetString(this byte[] b)
        {
            if (b == null) throw new ArgumentNullException(nameof(b));
            return (new UnicodeEncoding()).GetString(b, 0, b.Length);
        }

        public static byte[] SubByteArray(this byte[] source, int startIndex, int count = -1)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            var sourceLength = source.Length;
            if (startIndex < 0 || count < -1 || (count != -1 && startIndex + count >= sourceLength))
                throw new IndexOutOfRangeException();

            if (count == -1) count = sourceLength - startIndex;
            var retVal = new byte[count];
            for (var i = 0; i < count; i++)
                retVal[i] = source[i + startIndex];

            return retVal;
        }
    }
}