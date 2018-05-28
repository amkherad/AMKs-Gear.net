using System;
using System.Collections.Generic;
using System.Linq;
using AMKsGear.Architecture.Data.Types;

namespace AMKsGear.Core.TraceTools.Manipulation
{
    public class TestDataGeneratorSettings
    {
        public Gender GenderValue { get; set; }
        public int MinAgeValue { get; set; }
        public int MaxAgeValue { get; set; }

        public TestDataGeneratorSettings()
        {
            MinAgeValue = 0;
            MaxAgeValue = 120;

            GenderValue = Gender.Male |
                          Gender.Female;
        }

        public TestDataGeneratorSettings SetGender(Gender gender)
        {
            if (!((gender == Gender.Male) ||
                  (gender == Gender.Female) ||
                  (gender == (Gender.Male | Gender.Female))))
                throw new ArgumentOutOfRangeException(nameof(gender));
            GenderValue = gender;
            return this;
        }

        public TestDataGeneratorSettings SetMinAge(int minAge)
        {
            MinAgeValue = minAge;
            return this;
        }
        public TestDataGeneratorSettings SetMaxAge(int maxAge)
        {
            MaxAgeValue = maxAge;
            return this;
        }
    }

    public class TestDataContext
    {
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public int Age { get; protected set; }
        public DateTime BirthDate { get; protected set; }
        public string PhoneNumber { get; protected set; }
        public string Email { get; protected set; }

        public string FullName => $"{FirstName} {LastName}";

        private TestDataContext() { }

        public static IEnumerable<TestDataContext> Generate(int count, TestDataGeneratorSettings settings = null)
        {
            if (settings == null) settings = new TestDataGeneratorSettings();
            var random = new Random(Guid.NewGuid().GetHashCode());
            return Generate(count, random, settings);
        }
        public static IEnumerable<TestDataContext> Generate(int count, Random random, TestDataGeneratorSettings settings = null)
        {
            if (settings == null) settings = new TestDataGeneratorSettings();
            if (random == null) throw new ArgumentNullException(nameof(random));
            for (; count > 0; count--)
                yield return _generate(random, settings);
        }

        public static TestDataContext Generate(TestDataGeneratorSettings settings = null)
        {
            return _generate(new Random(Guid.NewGuid().GetHashCode()), settings);
        }
        public static TestDataContext Generate(Random random, TestDataGeneratorSettings settings = null)
        {
            if (random == null) throw new ArgumentNullException(nameof(random));
            return _generate(random, settings);
        }

        private static string _generateName(Random random, TestDataGeneratorSettings settings)
        {
            switch (settings.GenderValue)
            {
                case Gender.Male:
                    return MaleNames[random.Next(MaleNames.Length)];
                case Gender.Female:
                    return FemaleNames[random.Next(FemaleNames.Length)];
                default:
                    if (_allNames == null)
                        lock (_allNamesSyncLock)
                            if (_allNames == null)
                                _allNames = GetAllNames().ToArray();

                    return _allNames[random.Next(_allNames.Length)];
            }
        }
        private static TestDataContext _generate(Random random, TestDataGeneratorSettings settings)
        {
            var age = random.Next(settings.MinAgeValue, settings.MaxAgeValue);
            return new TestDataContext
            {
                FirstName = _generateName(random, settings),
                LastName = Family[random.Next(Family.Length)],
                Age = age,
                BirthDate = DateTime.Today.AddDays(-age),

            };
        }


        #region Data

        private static string[] _allNames;
        private static readonly object _allNamesSyncLock = new object();

        public static IEnumerable<string> GetAllNames()
        {
            foreach (var name in MaleNames) yield return name;
            foreach (var name in FemaleNames) yield return name;
        }

        public static readonly string[] MaleNames =
        {
            "میلاد",
            "علی",
            "حسین",
            "محمد",
            "کوروش",
            "اسفندیار",
            "سامان",
            "مهراد",
            "سهراب",
            "کامران",
            "مهران",
            "خشایار",
            "فرهاد",
            "داریوش",
            "بهرام",
            "مهدی",
            "یاسر",
            "جعفر",
            "حمید",
            "فواد",
            "رامین",
            "بهروز",
            "نیما",
            "برمک",
            "بهادر",
            "بهزاد",
            "بهمن",
            "بیژن",
            "زرین",
            "ساسان",
            "سامان",
            "سپنتا",
            "سپند",
            "سپهر",
            "سرافراز",
            "سروش",
            "سهند",
            "سیامک",
            "سیاوش",
            "پاکان",
            "پدرام",
            "پرویز",
            "پژمان",
            "پوریا",
            "پویا",
            "پیام",
            "پیمان",
            "گودرز",
            "لهراسب",
            "فربد",
            "فرخ",
            "فرداد",
            "فرزین",
            "فرشاد",
            "فیروز",
            "کاوه",
            "کیارش",
            "کیان",
            "کیخسرو",
            "نادر",
            "نوید",
            "ونداد",
            "هرمز",
            "همایون",
            "هوشنگ",
            "یزدان",
            "شادمهر",
            "شاهرخ",
            "شهروز",
            "آرین",
        };

        public static readonly string[] FemaleNames =
        {
            "مهرانه",
            "باران",
            "سارا",
            "زهرا",
            "مرضیه",
            "سمیه",
            "کیمیا",
            "سیما",
            "سیمین",
            "بارانه",
            "لاله",
            "فرزانه",
            "سمیرا",
            "سمانه",
            "مهربان",
            "مهین",
            "مهرناز",
            "عاطفه",
            "فتانه",
            "شیما",
            "بهار",
            "بهرخ",
            "زری",
            "زیبا",
            "ژاله",
            "ژینا",
            "ساناز",
            "سایه",
            "سپیده",
            "سودابه",
            "سوزان",
            "سوسن",
            "سوگند",
            "پافته آ",
            "پریچهر",
            "پریسا",
            "پرناز",
            "پریوش",
            "پریا",
            "پوپک",
            "پوران",
            "گلاره",
            "گلدخت",
            "گلی",
            "گیتی",
            "گیسو",
            "لادن",
            "فتانه",
            "فرانک",
            "فیروزه",
            "کتایون",
            "کیهانه",
            "نازی",
            "ناهید",
            "نرگس",
            "نسترن",
            "نسرین",
            "شیبا",
            "آتوسا",
            "آذر",
            "آذرنوش",
            "آرزو",
            "آرمیتا",
            "آزیتا",
            "آناهیتا",
            "آیدا",
        };

        public static readonly string[] Family =
        {
            "موسوی",
            "اسفندیاری",
            "تهرانی",
            "فروزانفر",
            "هاشمی",
            "مجیدی",
            "کیمیا",
            "مهرپویا",
            "مهرآذر",
            "مهرانفر",
            "هاطف",
            "کاظمی",
            "راد",
            "یوشیج",
            "نیازی",
            "واقعی",
            "بزرگمهر",
            "زرنگار",
            "زادفر",
            "گلشن",
            "گلچین",
            "گلریز",
            "فربد",
            "فرداد",
            "فرزانه",
            "فرزین",
            "فرشید",
            "فرنگیس",
            "فرنود",
            "فروتن",
            "فروزان",
            "فروزنده",
            "فیروز",
            "کارا",
            "کامبخش",
            "کامجو",
            "کیان",
            "نازآفرین",
            "نوش آذر",
            "نیکداد",
            "نیکدل",
            "نیلوفر",
            "همراه",
            "هویدا",
            "هیرمند",
            "یزدان",
            "شادان",
            "شایسته",
            "شهرآرا",
            "شهروز",
            "طهماسب",
            "تابان",
            "تارا",
            "آریا",
            "آریافر",
        };

        public static readonly string[] EmailProviders =
        {
            "gmail.com",
            "yahoo.com",
            "chmail.com",
            "outlook.com",
            "live.com",
            "mail.ru",
            "rocketmail.com",
            "hotmail.com",
        };

        #endregion
    }
}