//using WMS.framework.core.objects.attributes;
//using WMS.framework.data.Properties;

//namespace WMS.framework.data.validators
//{
//    public class DecimalDataValidator : DataValidator
//    {
//        public decimal MinValue { get; set; }
//        public decimal MaxValue { get; set; }

//        public DecimalDataValidator() { MinValue = decimal.MinValue; MaxValue = decimal.MaxValue; }
//        public DecimalDataValidator(decimal maxValue) { MinValue = decimal.MinValue; MaxValue = maxValue; }
//        public DecimalDataValidator(decimal minValue, decimal maxValue) { MinValue = minValue; MaxValue = maxValue; }

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

//            decimal decimal_val = ((decimal)value);
//            if (decimal_val == 0)
//            {
//                if (RequiredMode.IsRequired && RequiredMode.RequiredValidationMode == RequiredValidationModes.InvalidOnDefaultValue)
//                {
//                    error = string.Format(Translator.Translate(Resources.NullValue), FieldName);
//                    return false;
//                }
//            }
//            else if (decimal_val < MinValue)
//            {
//                error = string.Format(Translator.Translate(Resources.InvalidNumber), FieldName, decimal_val, MinValue, Translator.Translate(Resources.LessThan));
//                return false;
//            }
//            else if (decimal_val > MaxValue)
//            {
//                error = string.Format(Translator.Translate(Resources.InvalidNumber), FieldName, decimal_val, MaxValue, Translator.Translate(Resources.GreatThan));
//                return false;
//            }
//            error = Translator.Translate(Resources.ValidValue);
//            return true;
//        }
//    }
//}
