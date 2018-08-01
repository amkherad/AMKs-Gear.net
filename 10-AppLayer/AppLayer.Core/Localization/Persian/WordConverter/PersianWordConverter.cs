using System;

namespace AMKsGear.AppLayer.Core.Localization.Persian.WordConverter
{
    public class PersianWordConverter
    {
        private static readonly string[] yakan = new string[10] { "صفر", "یک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه" };
        private static readonly string[] dahgan = new string[10] { "", "", "بیست", "سی", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود" };
        private static readonly string[] dahyek = new string[10] { "ده", "یازده", "دوازده", "سیزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده" };
        private static readonly string[] sadgan = new string[10] { "", "یکصد", "دویست", "سیصد", "چهارصد", "پانصد", "ششصد", "هفتصد", "هشتصد", "نهصد" };
        private static readonly string[] basex = new string[5] { "", "هزار", "میلیون", "میلیارد", "تریلیون" };
        private static string Getnum3(int num3)
        {
            string s = "";
            int d3, d12;
            d12 = num3 % 100;
            d3 = num3 / 100;
            if (d3 != 0)
                s = sadgan[d3] + " و ";
            if ((d12 >= 10) && (d12 <= 19))
            {
                s = s + dahyek[d12 - 10];
            }
            else
            {
                int d2 = d12 / 10;
                if (d2 != 0)
                    s = s + dahgan[d2] + " و ";
                int d1 = d12 % 10;
                if (d1 != 0)
                    s = s + yakan[d1] + " و ";
                s = s.Substring(0, s.Length - 3);
            };
            return s;
        }

        public string NumberToString(long number) => _NumberToString(number.ToString());
        public string NumberToString(string number)
        {
            long lngNum;
            if (!long.TryParse(number, out lngNum))
                throw new ArgumentException();

            return _NumberToString(number);
        }
        private string _NumberToString(string number)
        {
            var stotal = "";
            if (number == "") return "صفر";
            if (number == "0")
            {
                return yakan[0];
            }
            else
            {
                if (number.StartsWith("-"))
                {
                    number = number.Trim('-', ' ');
                    stotal = "منفی ";
                }

                number = number.PadLeft(((number.Length - 1) / 3 + 1) * 3, '0');
                int L = number.Length / 3 - 1;
                for (int i = 0; i <= L; i++)
                {
                    int b = int.Parse(number.Substring(i * 3, 3));
                    if (b != 0)
                        stotal = stotal + Getnum3(b) + " " + basex[L - i] + " و ";
                }
                stotal = stotal.Substring(0, stotal.Length - 3);
            }
            return stotal;
        }
    }
}