//using WMS.framework.data.validators;

//namespace AMKsGear.Core.Data.Validation.Validators
//{
//    public class BinaryDataValidator : DataValidator
//    {
//        public int MinLength { get; set; }
//        public int MaxLength { get; set; }

//        public BinaryDataValidator() { MinLength = int.MinValue; MaxLength = int.MaxValue; }
//        public BinaryDataValidator(int minLength, int maxLength) { MinLength = minLength; MaxLength = maxLength; }

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

//            byte[] ba = (value as byte[]);
//            if (ba.Length == 0)
//            {
//                if (RequiredMode.IsRequired && RequiredMode.RequiredValidationMode == RequiredValidationModes.InvalidOnDefaultValue)
//                {
//                    error = string.Format(Translator.Translate(Resources.NullValue), FieldName);
//                    return false;
//                }
//            }
//            else if (ba.Length < MinLength)
//            {
//                error = string.Format(Resources.InvalidStringBounds, FieldName, MinLength, Translator.Translate(Resources.LessThan));
//                return false;
//            }
//            else if (ba.Length > MaxLength)
//            {
//                error = string.Format(Translator.Translate(Resources.InvalidStringBounds), FieldName, MaxLength, Translator.Translate(Resources.GreatThan));
//                return false;
//            }
//            error = Translator.Translate(Resources.ValidValue);
//            return true;
//        }
//    }
//}
