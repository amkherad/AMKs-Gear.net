//using WMS.framework.core.objects.attributes;
//using WMS.framework.data.Properties;

//namespace WMS.framework.data.validators
//{
//    public class FloatDataValidator : DataValidator
//    {
//        public float MinValue { get; set; }
//        public float MaxValue { get; set; }

//        public FloatDataValidator() { MinValue = float.MinValue; MaxValue = float.MaxValue; }
//        public FloatDataValidator(float maxValue) { MinValue = float.MinValue; MaxValue = maxValue; }
//        public FloatDataValidator(float minValue, float maxValue) { MinValue = minValue; MaxValue = maxValue; }

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

//            float float_val = ((float)value);
//            if (float_val == 0)
//            {
//                if (RequiredMode.IsRequired && RequiredMode.RequiredValidationMode == RequiredValidationModes.InvalidOnDefaultValue)
//                {
//                    error = string.Format(Translator.Translate(Resources.NullValue), FieldName);
//                    return false;
//                }
//            }
//            else if (float_val < MinValue)
//            {
//                error = string.Format(Translator.Translate(Resources.InvalidNumber), FieldName, float_val, MinValue, Translator.Translate(Resources.LessThan));
//                return false;
//            }
//            else if (float_val > MaxValue)
//            {
//                error = string.Format(Translator.Translate(Resources.InvalidNumber), FieldName, float_val, MaxValue, Translator.Translate(Resources.GreatThan));
//                return false;
//            }
//            error = Translator.Translate(Resources.ValidValue);
//            return true;
//        }
//    }
//}
