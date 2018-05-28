//using System;
//using WMS.framework.core.objects.attributes;
//using WMS.framework.data.validators;

//namespace WMS.framework.data.modelbase.attributes
//{
//    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
//    public class StringValidationAttribute : ValidationAttribute
//    {
//        public int MinLength { get; set; }
//        public int MaxLength { get; set; }
//        public string RegexPattern { get; set; }
//        public bool AllowEmptyString { get; set; }

//        public StringValidationAttribute() { MinLength = 0; MaxLength = int.MaxValue; RequiredMode = new RequiredModes(); AllowEmptyString = true; }
//        public StringValidationAttribute(bool nullable = true) { MinLength = 0; MaxLength = int.MaxValue; RequiredMode = new RequiredModes(!nullable); AllowEmptyString = true; }
//        public StringValidationAttribute(int maxLength, bool nullable = true) { MinLength = 0; MaxLength = maxLength; RequiredMode = new RequiredModes(!nullable); AllowEmptyString = true; }
//        public StringValidationAttribute(int minLength, int maxLength, bool nullable = true) { MinLength = minLength; MaxLength = maxLength; RequiredMode = new RequiredModes(!nullable); AllowEmptyString = true; }

//        public override DataValidator Validator
//        {
//            get
//            {
//                StringDataValidator sdv = new StringDataValidator(MinLength, MaxLength, RegexPattern);
//                sdv.RequiredMode = RequiredMode;
//                return sdv;
//            }
//        }
//    }
//}
