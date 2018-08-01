using System.Text.RegularExpressions;
using AMKsGear.Core.Localization;

namespace AMKsGear.Core.Data.Validation.Validators
{
    public class StringDataValidator : DataValidator
    {
        public int MinLength { get; set; }
        public int MaxLength { get; set; }
        public string RegexPattern { get; set; }

        public StringDataValidator() { MinLength = int.MinValue; MaxLength = int.MaxValue; }
        public StringDataValidator(int minLength, int maxLength) { MinLength = minLength; MaxLength = maxLength; }
        public StringDataValidator(int minLength, int maxLength, string regexPattern) { MinLength = minLength; MaxLength = maxLength; RegexPattern = regexPattern; }
        public StringDataValidator(string regexPattern) { MinLength = 0; MaxLength = int.MaxValue; RegexPattern = regexPattern; }

        public static bool ValidateRegexPattern(string regexPattern, string value)
        {
            try
            {
                return Regex.IsMatch(value, regexPattern);
            }
            catch
            {
                return false;
            }
        }

        public override bool Validate(object value, out ITranslateResult result)
        {
//            if (value == null)
//            {
//                if (RequiredMode.IsRequired && (RequiredMode.RequiredValidationMode == RequiredValidationModes.InvalidOnDefaultValue))
//                {
//                    error = string.Format(Translator.Translate(Resources.NullValue), FieldName);
//                    return false;
//                }
//                else
//                {
//                    error = Translator.Translate(Resources.ValidValue);
//                    return true;
//                }
//            }

//            string str = ((string)value);
//            if(str.Length == 0)
//            {
//                if (RequiredMode.IsRequired && RequiredMode.RequiredValidationMode == RequiredValidationModes.InvalidOnDefaultValue)
//                {
//                    error = string.Format(Translator.Translate(Resources.NullValue), FieldName);
//                    return false;
//                }
//            }
//            else if (str.Length < MinLength)
//            {
//                error = string.Format(Translator.Translate(Resources.InvalidStringBounds), FieldName, str, MinLength, Translator.Translate(Resources.LessThan));
//                return false;
//            }
//            else if (str.Length > MaxLength)
//            {
//                error = string.Format(Translator.Translate(Resources.InvalidStringBounds), FieldName, str, MaxLength, Translator.Translate(Resources.GreatThan));
//                return false;
//            }
//            else if (!string.IsNullOrWhiteSpace(RegexPattern))
//            {
//                bool regex_result = false;
//                try
//                {
//                    regex_result = Regex.IsMatch(str, RegexPattern);
//                }
//                catch { }
//                if (!regex_result)
//                {
//                    error = string.Format(Translator.Translate(Resources.InvalidStringPattern), FieldName, str);
//                    return false;
//                }
//            }

            result = LocalizationServices.FormatLazy<IValidationLocalization, DefaultValidationLocalization>(x => x.ValidValueString);
            return true;
        }
    }
}
