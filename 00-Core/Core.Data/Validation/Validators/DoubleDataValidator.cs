//using WMS.framework.core.objects.attributes;
//using WMS.framework.data.Properties;

//namespace WMS.framework.data.validators
//{
//    public class DoubleDataValidator : DataValidator
//    {
//        public double MinValue { get; set; }
//        public double MaxValue { get; set; }

//        public DoubleDataValidator() { MinValue = double.MinValue; MaxValue = double.MaxValue; }
//        public DoubleDataValidator(double maxValue) { MinValue = double.MinValue; MaxValue = maxValue; }
//        public DoubleDataValidator(double minValue, double maxValue) { MinValue = minValue; MaxValue = maxValue; }

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

//            double double_val = ((double)value);
//            if (double_val == 0)
//            {
//                if (RequiredMode.IsRequired && RequiredMode.RequiredValidationMode == RequiredValidationModes.InvalidOnDefaultValue)
//                {
//                    error = string.Format(Translator.Translate(Resources.NullValue), FieldName);
//                    return false;
//                }
//            }
//            else if (double_val < MinValue)
//            {
//                error = string.Format(Translator.Translate(Resources.InvalidNumber), FieldName, double_val, MinValue, Translator.Translate(Resources.LessThan));
//                return false;
//            }
//            else if (double_val > MaxValue)
//            {
//                error = string.Format(Translator.Translate(Resources.InvalidNumber), FieldName, double_val, MaxValue, Translator.Translate(Resources.GreatThan));
//                return false;
//            }
//            error = Translator.Translate(Resources.ValidValue);
//            return true;
//        }
//    }
//}
