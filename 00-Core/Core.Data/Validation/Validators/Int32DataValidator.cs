//using WMS.framework.core.objects.attributes;
//using WMS.framework.data.Properties;

//namespace WMS.framework.data.validators
//{
//    public class Int32DataValidator : DataValidator
//    {
//        public int MinValue { get; set; }
//        public int MaxValue { get; set; }

//        public Int32DataValidator() { MinValue = int.MinValue; MaxValue = int.MaxValue; }
//        public Int32DataValidator(int maxValue) { MinValue = int.MinValue; MaxValue = maxValue; }
//        public Int32DataValidator(int minValue, int maxValue) { MinValue = minValue; MaxValue = maxValue; }

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

//            int int_val = ((int)value);
//            if (int_val == 0)
//            {
//                if (RequiredMode.IsRequired && RequiredMode.RequiredValidationMode == RequiredValidationModes.InvalidOnDefaultValue)
//                {
//                    error = string.Format(Translator.Translate(Resources.NullValue), FieldName);
//                    return false;
//                }
//            }
//            else if (int_val < MinValue)
//            {
//                error = string.Format(Translator.Translate(Resources.InvalidNumber), FieldName, int_val, MinValue, Translator.Translate(Resources.LessThan));
//                return false;
//            }
//            else if (int_val > MaxValue)
//            {
//                error = string.Format(Translator.Translate(Resources.InvalidNumber), FieldName, int_val, MaxValue, Translator.Translate(Resources.GreatThan));
//                return false;
//            }
//            error = Translator.Translate(Resources.ValidValue);
//            return true;
//        }
//    }
//}
