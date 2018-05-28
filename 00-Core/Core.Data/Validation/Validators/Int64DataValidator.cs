//using WMS.framework.core.objects.attributes;
//using WMS.framework.data.Properties;

//namespace WMS.framework.data.validators
//{
//    public class Int64DataValidator : DataValidator
//    {
//        public long MinValue { get; set; }
//        public long MaxValue { get; set; }

//        public Int64DataValidator() { MinValue = long.MinValue; MaxValue = long.MaxValue; }
//        public Int64DataValidator(long maxValue) { MinValue = long.MinValue; MaxValue = maxValue; }
//        public Int64DataValidator(long minValue, long maxValue) { MinValue = minValue; MaxValue = maxValue; }

//        public override bool Validate(object value, out string error)
//        {
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

//            long long_val = ((long)value);
//            if (long_val == 0)
//            {
//                if (RequiredMode.IsRequired && RequiredMode.RequiredValidationMode == RequiredValidationModes.InvalidOnDefaultValue)
//                {
//                    error = string.Format(Translator.Translate(Resources.NullValue), FieldName);
//                    return false;
//                }
//            }
//            else if (long_val < MinValue)
//            {
//                error = string.Format(Translator.Translate(Resources.InvalidNumber), FieldName, long_val, MinValue, Translator.Translate(Resources.LessThan));
//                return false;
//            }
//            else if (long_val > MaxValue)
//            {
//                error = string.Format(Translator.Translate(Resources.InvalidNumber), FieldName, long_val, MaxValue, Translator.Translate(Resources.GreatThan));
//                return false;
//            }
//            error = Translator.Translate(Resources.ValidValue);
//            return true;
//        }
//    }
//}
