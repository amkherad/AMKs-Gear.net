namespace AMKsGear.AppLayer.Core.Globalization.Persian.PersianDateTime
{
    internal static class PDateTimeUtil
    {
        /// <summary>
        /// Adds a preceding zero to single day or months
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        internal static string ToDouble(int i)
        {
            if (i > 9)
            {
                return i.ToString();
            }
            else
            {
                return "0" + i.ToString();
            }
        }
    }
}
