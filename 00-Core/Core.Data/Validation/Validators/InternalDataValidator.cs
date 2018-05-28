//using System.Collections.Generic;
//using WMS.framework.core.objects.attributes;
//using WMS.framework.data.Properties;
//using da = System.ComponentModel.DataAnnotations;

//namespace WMS.framework.data.validators
//{
//    public class InternalDataValidator : DataValidator
//    {
//        public object Instance { get; set; }
//        public List<da.ValidationAttribute> Validators { get; protected set; }

//        public InternalDataValidator() { Validators = new List<da.ValidationAttribute>(); }
//        public InternalDataValidator(object instance, List<da.ValidationAttribute> validators) { Instance = instance; Validators = validators; }

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

//            try
//            {
//                foreach (da.ValidationAttribute va in Validators)
//                    va.Validate(value, FieldName);
//                error = Translator.Translate(Resources.ValidValue);
//                return true;
//            }
//            catch (da.ValidationException ex)
//            {
//                error = Translator.Translate(ex.ValidationResult.ErrorMessage);
//                return false;
//            }
//        }
//    }
//}
