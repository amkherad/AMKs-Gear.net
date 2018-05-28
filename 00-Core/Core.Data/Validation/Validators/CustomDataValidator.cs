//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace WMS.framework.data.validators
//{
//    public class CustomDataValidator : DataValidator
//    {
//        public delegate bool CustomDataValidatorPredicate(object value, out string error);
//        public delegate bool CustomDataValidatorPredicate2(object tag);
//        public object Tag { get; set; }
//        public CustomDataValidatorPredicate Predicate { get; protected set; }
        
//        public CustomDataValidator() { }
//        public CustomDataValidator(object tag) { Tag = tag; }
//        public CustomDataValidator(object tag, CustomDataValidatorPredicate predicate) { Tag = tag; Predicate = predicate; }

//        public override bool Validate(object value, out string error)
//        {
//            if (!DefaultDataValidator.Validate(this, value, out error))
//                return false;

//            var predicate = Predicate;
//            if (predicate != null)
//                return predicate(value, out error);
//            error = Translator.Translate(Properties.Resources.ValidValue);
//            return true;
//        }
//        public bool Validate(CustomDataValidatorPredicate2 predicate) { return predicate(Tag); }
//    }
//}
