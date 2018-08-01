using AMKsGear.Architecture.Localization;

namespace AMKsGear.Core.Data.Validation
{
    public interface IValidationLocalization : ILocalizationModel
    {
        string InvalidBinaryString { get; }
        string InvalidDateTimeString { get; }
        string InvalidNumberString { get; }
        string InvalidStringLengthString { get; }
        string InvalidStringPatternString { get; }
        string InvalidValueString { get; }
        string LessThanString { get; }
        string GreaterThanString { get; }
        string NullValueString { get; }
        string ValidValueString { get; }
    }
    public class DefaultValidationLocalization : DefaultEnglishLocalization, IValidationLocalization
    {
        public string InvalidBinaryString => "Field '{0}', binary length is {2} expected value '{1}'.";
        public string InvalidDateTimeString => "Field '{0}', datetime '{1}' range is {3} expected value '{2}'.";
        public string InvalidNumberString => "Field '{0}', number '{1}' range is {3} expected value '{2}'.";
        public string InvalidStringLengthString => "Field '{0}', string '{1}' length is {3} expected value '{2}'.";
        public string InvalidStringPatternString => "Field '{0}', string pattern '{1}' is invalid.";
        public string InvalidValueString => "Invalid value.";
        public string LessThanString => "Less than";
        public string GreaterThanString => "Greater than";
        public string NullValueString => "Null value";
        public string ValidValueString => "Valid value.";
    }
}