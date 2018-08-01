using AMKsGear.Architecture.Localization;

namespace AMKsGear.AppLayer.Core.Localization.Persian.PersianDateTime
{
    public interface IPersianDateTimeLocalizationModel : ILocalizationModel
    {
        string InvalidDateFormat { get; }
        string InvalidDateTime { get; }
        string InvalidDateTimeLength { get; }
        string InvalidDay { get; }
        string InvalidEra { get; }
        string InvalidFourDigitYear { get; }
        string InvalidHour { get; }
        string InvalidLeapYear { get; }
        string InvalidMinute { get; }
        string InvalidMonth { get; }
        string InvalidMonthDay { get; }
        string InvalidSecond { get; }
        string InvalidMillisecond { get; }
        string InvalidTimeFormat { get; }
        string InvalidYear { get; }

        string Number0 { get; }
        string Number1 { get; }
        string Number2 { get; }
        string Number3 { get; }
        string Number4 { get; }
        string Number5 { get; }
        string Number6 { get; }
        string Number7 { get; }
        string Number8 { get; }
        string Number9 { get; }
    }
    public class DefaultPersianDateTimeLocalizationModel : DefaultEnglishLocalization, IPersianDateTimeLocalizationModel
    {
        string IPersianDateTimeLocalizationModel.InvalidDateFormat => "";
        public string InvalidDateTime => "Invalid date time format.";
        public string InvalidDateTimeLength => "Invalid date time length.";
        public string InvalidDay => "Invalid day in date time.";
        public string InvalidEra => "Invalid era encountered.";
        public string InvalidFourDigitYear => "Invalid year encountered.";
        public string InvalidHour => "Invalid hour in date time.";
        public string InvalidLeapYear => "Invalid leap year in date time.";
        public string InvalidMinute => "Invalid minute in date time.";
        public string InvalidMonth => "Invalid month in date time.";
        public string InvalidMonthDay => "Invalid month day in date time.";
        public string InvalidSecond => "Invalid second in date time.";
        public string InvalidMillisecond => "Invalid millisecond in date time.";
        public string InvalidTimeFormat => "Invalid time format.";
        public string InvalidYear => "Invalid year in date time.";

        public string Number0 => "0";
        public string Number1 => "1";
        public string Number2 => "2";
        public string Number3 => "3";
        public string Number4 => "4";
        public string Number5 => "5";
        public string Number6 => "6";
        public string Number7 => "7";
        public string Number8 => "8";
        public string Number9 => "9";
    }
    public class ParsiPersianDateTimeLocalizationModel : DefaultEnglishLocalization, IPersianDateTimeLocalizationModel
    {
        string IPersianDateTimeLocalizationModel.InvalidDateFormat => "ساختار تاریخ مجاز نمیباشد.";
        public string InvalidDateTime => "مقدار زمان/ساعت صحیح نمیباشد.";
        public string InvalidDateTimeLength => "متن وارد شده برای زمان/ساعت صحیح نمیباشد.";
        public string InvalidDay => "مقدار روز صحیح نمیباشد.";
        public string InvalidEra => "محدوده وارد شده صحیح نمیباشد.";
        public string InvalidFourDigitYear => "مقدار وارد شده را نمیتوان به سال تبدیل کرد.";
        public string InvalidHour => "مقدار ساعت صحیح نمیباشد.";
        public string InvalidLeapYear  => "این سال ، سال کبیسه نیست. مقدار روز صحیح نمیباشد.";
        public string InvalidMinute => "مقدار دقیقه صحیح نمیباشد.";
        public string InvalidMonth  => "مقدار ماه صحیح نمیباشد.";
        public string InvalidMonthDay => "مقدار ماه/روز صحیح نمیباشد.";
        public string InvalidSecond => "مقدار ثانیه صحیح نمیباشد.";
        public string InvalidMillisecond => "مقدار صدم ثانیه صحیح نمیباشد.";
        public string InvalidTimeFormat  => "ساختار زمان صحیح نمیباشد.";
        public string InvalidYear => "مقدار سال صحیح نمیباشد.";
        public string Number0 => "۰";
        public string Number1 => "۱";
        public string Number2 => "۲";
        public string Number3 => "۳";
        public string Number4 => "۴";
        public string Number5 => "۵";
        public string Number6 => "۶";
        public string Number7 => "۷";
        public string Number8 => "۸";
        public string Number9 => "۹";
    }
}