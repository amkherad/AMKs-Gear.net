//using System;
//using WMS.framework.core.objects.attributes;
//using WMS.framework.data.validators;

//namespace WMS.framework.data.modelbase.attributes
//{
//    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
//    public class Int32ValidationAttribute : ValidationAttribute
//    {
//        public int Minimum { get; set; }
//        public int Maximum { get; set; }

//        public Int32ValidationAttribute() { Minimum = int.MinValue; Maximum = int.MaxValue; RequiredMode = new RequiredModes(); }
//        public Int32ValidationAttribute(bool nullable) { Minimum = int.MinValue; Maximum = int.MaxValue; RequiredMode = new RequiredModes(!nullable); }
//        public Int32ValidationAttribute(int minimum, bool nullable) { Minimum = minimum; Maximum = int.MaxValue; RequiredMode = new RequiredModes(!nullable); }
//        public Int32ValidationAttribute(int minimum, int maximum, bool nullable) { Minimum = minimum; Maximum = maximum; RequiredMode = new RequiredModes(!nullable); }

//        public override DataValidator Validator
//        {
//            get
//            {
//                Int32DataValidator sdv = new Int32DataValidator(Minimum, Maximum);
//                sdv.RequiredMode = RequiredMode;
//                return sdv;
//            }
//        }
//    }
//}
