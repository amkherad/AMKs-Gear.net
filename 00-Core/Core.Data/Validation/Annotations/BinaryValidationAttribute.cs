//using System;
//using WMS.framework.data.modelbase.attributes;

//namespace AMKsGear.Core.Data.Validation.Annotations
//{
//    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
//    public class BinaryValidationAttribute : ValidationAttribute
//    {
//        public int MinLength { get; set; }
//        public int MaxLength { get; set; }

//        public BinaryValidationAttribute() { MinLength = 0; MaxLength = int.MaxValue; RequiredMode = new RequiredModes(); }
//        public BinaryValidationAttribute(bool nullable = true) { MinLength = 0; MaxLength = int.MaxValue; RequiredMode = new RequiredModes(!nullable); }
//        public BinaryValidationAttribute(int maxLength, bool nullable = true) { MinLength = 0; MaxLength = maxLength; RequiredMode = new RequiredModes(!nullable); }
//        public BinaryValidationAttribute(int minLength, int maxLength, bool nullable = true) { MinLength = minLength; MaxLength = maxLength; RequiredMode = new RequiredModes(!nullable); }

//        public override DataValidator Validator
//        {
//            get
//            {
//                BinaryDataValidator sdv = new BinaryDataValidator(MinLength, MaxLength);
//                sdv.RequiredMode = RequiredMode;
//                return sdv;
//            }
//        }
//    }
//}
