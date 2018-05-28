//using System;
//using WMS.framework.core.objects.attributes;
//using WMS.framework.data.Properties;

//namespace WMS.framework.data.validators
//{
//    public class DateTimeDataValidator : DataValidator
//    {
//        public DateTime MinValue { get; set; }
//        public DateTime MaxValue { get; set; }

//        public DateTimeDataValidator() { MinValue = DateTime.MinValue; MaxValue = DateTime.MaxValue; }
//        public DateTimeDataValidator(DateTime maxValue) { MinValue = DateTime.MinValue; MaxValue = maxValue; }
//        public DateTimeDataValidator(DateTime minValue, DateTime maxValue) { MinValue = minValue; MaxValue = maxValue; }

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

//            DateTime dt_val = ((DateTime)value);
//            if (dt_val == default(DateTime))
//            {
//                if (RequiredMode.IsRequired && RequiredMode.RequiredValidationMode == RequiredValidationModes.InvalidOnDefaultValue)
//                {
//                    error = string.Format(Translator.Translate(Resources.NullValue), FieldName);
//                    return false;
//                }
//            }
//            else if (dt_val < MinValue)
//            {
//                error = string.Format(Translator.Translate(Resources.InvalidDateTime), FieldName, dt_val.ToLongDateString(), MinValue, Translator.Translate(Resources.LessThan));
//                return false;
//            }
//            else if (dt_val > MaxValue)
//            {
//                error = string.Format(Translator.Translate(Resources.InvalidDateTime), FieldName, dt_val.ToLongDateString(), MaxValue, Translator.Translate(Resources.GreatThan));
//                return false;
//            }
//            error = Translator.Translate(Resources.ValidValue);
//            return true;
//        }
//    }
//}
