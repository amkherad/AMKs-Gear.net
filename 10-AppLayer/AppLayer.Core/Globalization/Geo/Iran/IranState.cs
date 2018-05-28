using System;
using System.Collections.Generic;

namespace AMKsGear.AppLayer.Core.Globalization.Geo.Iran
{
    public class IranState
    {
        public string Name { get; }
        public string ParsiName { get; }
        public string PhonePrefix { get; }
        public string CarUIdPrefix { get; }
        public string MainCity { get; }
        public Dialect[] Dialects { get; }

        public IranState(string name, string phonePrefix, string parsiName, string mainCity, Dialect[] dialects)
        {
            Name = name;
            PhonePrefix = phonePrefix;
            ParsiName = parsiName;
            MainCity = mainCity;
            Dialects = dialects;
        }
        
        public static IEnumerable<IranState> GetProvinces(ProvinceType type)
        {
            if (type.HasFlag(ProvinceType.Province))
            {
                foreach (var prov in Provinces)
                    yield return prov;
            }
            if (type.HasFlag(ProvinceType.Island))
            {
                foreach (var prov in Islands)
                    yield return prov;
            }
        }



        public const string EastAzerbaijanName = "East-Azerbaijan";
        public const string WestAzerbaijanName = "West-Azerbaijan";
        public const string ArdebilName = "Ardebil";
        public const string IsfahanName = "Isfahan";
        public const string AlborzName = "Alborz";
        public const string IlamName = "Ilam";
        public const string BushehrName = "Bushehr";
        public const string TehranName = "Tehran";
        public const string ChaharMahaalBakhtiariName = "Chahar-Mahaal-Bakhtiari";
        public const string SouthKhorasanName = "South-Khorasan";
        public const string RazaviKhorasanName = "Razavi-Khorasan";
        public const string NorthKhorasanName = "North-Khorasan";
        public const string KhuzestanName = "Khuzestan";
        public const string ZanjanName = "Zanjan";
        public const string SemnanName = "Semnan";
        public const string SistanBaluchestanName = "Sistan-Baluchestan";
        public const string FarsName = "Fars";
        public const string QazvinName = "Qazvin";
        public const string QomName = "Qom";
        public const string KurdistanName = "Kurdistan";
        public const string KermanName = "Kerman";
        public const string KermanshahName = "Kermanshah";
        public const string KohgiluyehBoyerAhmadName = "Kohgiluyeh-Boyer-Ahmad";
        public const string GolestanName = "Golestan";
        public const string GilanName = "Gilan";
        public const string LorestanName = "Lorestan";
        public const string MazandaranName = "Mazandaran";
        public const string MarkaziName = "Markazi";
        public const string HormozganName = "Hormozgan";
        public const string HamadanName = "Hamadan";
        public const string YazdName = "Yazd";

        public const string AbuMusaName = "Abu-Musa";
        public const string QeshmName = "Qeshm";
        public const string BigFarorName = "Big-Faror";
        public const string SmallFarorName = "Small-Faror";
        public const string HendorabiName = "Hendorabi";
        public const string HengamName = "Hengam";
        public const string HormozName = "Hormoz";
        public const string KharkName = "Khark";
        public const string KishName = "Kish";
        public const string LarkName = "Lark";
        public const string LavanName = "Lavan";
        public const string SiriName = "Siri";
        public const string BigTunbName = "Big-Tunb";
        public const string SmallTunbName = "Small-Tunb";


        public static IranState EastAzerbaijan => new IranState(EastAzerbaijanName, "", "آذربایجان شرقی", null, new[] { Dialect.Parsi, Dialect.TorkiAzari });
        public static IranState WestAzerbaijan => new IranState(WestAzerbaijanName, "", "آذربایجان غربی", null, new[] { Dialect.Parsi, Dialect.TorkiAzari });
        public static IranState Ardebil => new IranState(ArdebilName, "", "اردبیل", null, new[] { Dialect.Parsi, Dialect.TorkiAzari });
        public static IranState Isfahan => new IranState(IsfahanName, "", "اصفهان", null, new[] { Dialect.Parsi, Dialect.ParsiIsfahani });
        public static IranState Alborz => new IranState(AlborzName, "", "البرز", null, new[] { Dialect.Parsi });
        public static IranState Ilam => new IranState(IlamName, "", "ایلام", null, new[] { Dialect.Parsi });
        public static IranState Bushehr => new IranState(BushehrName, "", "بوشهر", null, new[] { Dialect.Parsi });
        public static IranState Tehran => new IranState(TehranName, "", "تهران", null, new[] { Dialect.Parsi });
        public static IranState ChaharMahaalBakhtiari => new IranState(ChaharMahaalBakhtiariName, "", "چهارمحال و بختیاری", null, new[] { Dialect.Parsi });
        public static IranState SouthKhorasan => new IranState(SouthKhorasanName, "", "خراسان جنوبی", null, new[] { Dialect.Parsi });
        public static IranState RazaviKhorasan => new IranState(RazaviKhorasanName, "", "خراسان رضوی", null, new[] { Dialect.Parsi });
        public static IranState NorthKhorasan => new IranState(NorthKhorasanName, "", "خراسان شمالی", null, new[] { Dialect.Parsi });
        public static IranState Khuzestan => new IranState(KhuzestanName, "", "خوزستان", null, new[] { Dialect.Parsi });
        public static IranState Zanjan => new IranState(ZanjanName, "", "زنجان", null, new[] { Dialect.Parsi });
        public static IranState Semnan => new IranState(SemnanName, "", "سمنان", null, new[] { Dialect.Parsi });
        public static IranState SistanBaluchestan => new IranState(SistanBaluchestanName, "", "سیستان و بلوچستان", null, new[] { Dialect.Parsi });
        public static IranState Fars => new IranState(FarsName, "", "فارس", null, new[] { Dialect.Parsi });
        public static IranState Qazvin => new IranState(QazvinName, "", "قزوین", null, new[] { Dialect.Parsi });
        public static IranState Qom => new IranState(QomName, "", "قم", null, new[] { Dialect.Parsi });
        public static IranState Kurdistan => new IranState(KurdistanName, "", "کردستان", null, new[] { Dialect.Parsi });
        public static IranState Kerman => new IranState(KermanName, "", "کرمان", null, new[] { Dialect.Parsi });
        public static IranState Kermanshah => new IranState(KermanshahName, "", "کرمانشاه", null, new[] { Dialect.Parsi });
        public static IranState KohgiluyehBoyerAhmad => new IranState(KohgiluyehBoyerAhmadName, "", "کهگیلویه و بویراحمد", null, new[] { Dialect.Parsi });
        public static IranState Golestan => new IranState(GolestanName, "", "گلستان", null, new[] { Dialect.Parsi });
        public static IranState Gilan => new IranState(GilanName, "", "گیلان", null, new[] { Dialect.Parsi });
        public static IranState Lorestan => new IranState(LorestanName, "", "لرستان", null, new[] { Dialect.Parsi });
        public static IranState Mazandaran => new IranState(MazandaranName, "", "مازندران", null, new[] { Dialect.Parsi });
        public static IranState Markazi => new IranState(MarkaziName, "", "مرکزی", null, new[] { Dialect.Parsi });
        public static IranState Hormozgan => new IranState(HormozganName, "", "هرمزگان", null, new[] { Dialect.Parsi });
        public static IranState Hamadan => new IranState(HamadanName, "", "همدان", null, new[] { Dialect.Parsi });
        public static IranState Yazd => new IranState(YazdName, "", "یزد", null, new[] { Dialect.Parsi });

        public static IranState AbuMusa => new IranState(AbuMusaName, "", "ابوموسی", null, new[] { Dialect.Parsi });
        public static IranState Qeshm => new IranState(QeshmName, "", "قشم", null, new[] { Dialect.Parsi });
        public static IranState BigFaror => new IranState(BigFarorName, "", "فرور بزرگ", null, new[] { Dialect.Parsi });
        public static IranState SmallFaror => new IranState(SmallFarorName, "", "فرور کوچک", null, new[] { Dialect.Parsi });
        public static IranState Hendorabi => new IranState(HendorabiName, "", "هندروابی", null, new[] { Dialect.Parsi });
        public static IranState Hengam => new IranState(HengamName, "", "هنگام", null, new[] { Dialect.Parsi });
        public static IranState Hormoz => new IranState(HormozName, "", "هرمز", null, new[] { Dialect.Parsi });
        public static IranState Khark => new IranState(KharkName, "", "خارک", null, new[] { Dialect.Parsi });
        public static IranState Kish => new IranState(KishName, "", "کیش", null, new[] { Dialect.Parsi });
        public static IranState Lark => new IranState(LarkName, "", "لارک", null, new[] { Dialect.Parsi });
        public static IranState Lavan => new IranState(LavanName, "", "لاوان", null, new[] { Dialect.Parsi });
        public static IranState Siri => new IranState(SiriName, "", "سیری", null, new[] { Dialect.Parsi });
        public static IranState BigTunb => new IranState(BigTunbName, "", "تنب بزرگ", null, new[] { Dialect.Parsi });
        public static IranState SmallTunb = new IranState(SmallTunbName, "", "تنب کوچک", null, new[] { Dialect.Parsi });

        public static IranState[] Provinces => new[]
        {
            EastAzerbaijan,
            WestAzerbaijan,
            Ardebil,
            Isfahan,
            Alborz,
            Ilam ,
            Bushehr,
            Tehran,
            ChaharMahaalBakhtiari,
            SouthKhorasan,
            RazaviKhorasan,
            NorthKhorasan,
            Khuzestan,
            Zanjan,
            Semnan,
            SistanBaluchestan,
            Fars,
            Qazvin,
            Qom,
            Kurdistan,
            Kerman,
            Kermanshah,
            KohgiluyehBoyerAhmad,
            Golestan,
            Gilan,
            Lorestan,
            Mazandaran,
            Markazi,
            Hormozgan,
            Hamadan,
            Yazd,
        };

        public static IranState[] Islands => new[]
        {
            AbuMusa,
            Qeshm,
            BigFaror,
            SmallFaror,
            Hendorabi,
            Hengam,
            Hormoz,
            Khark,
            Kish,
            Lark,
            Lavan,
            Siri,
            BigTunb,
            SmallTunb
        };
    }
    
    [Flags]
    public enum ProvinceType
    {
        Province,
        Island,
        All = Province | Island
    }
}