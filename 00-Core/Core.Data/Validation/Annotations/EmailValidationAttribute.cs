//using System;
//using WMS.framework.core.objects.attributes;
//using WMS.framework.data.validators;

//namespace WMS.framework.data.modelbase.attributes
//{
//    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
//    public class EmailValidationAttribute : ValidationAttribute
//    {
//        public string RegexPattern { get; set; }

//        public EmailValidationAttribute() { RequiredMode.RequiredValidationMode = RequiredValidationModes.InvalidOnDefaultValue; }
//        public EmailValidationAttribute(bool nullable) { RequiredMode = new RequiredModes(!nullable,true); }

//        public override DataValidator Validator
//        {
//            get
//            {
//                StringDataValidator sdv = new StringDataValidator(StringDataValidator.EmailRegexPattern);
//                sdv.RequiredMode = RequiredMode;
//                return sdv;
//            }
//        }
//    }
//}
