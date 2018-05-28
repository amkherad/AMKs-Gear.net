using System;
using AMKsGear.Architecture.Annotations;
using AMKsGear.Core.Data.Validation.Validators;

namespace AMKsGear.Core.Data.Validation.Annotations
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public abstract class ValidationAttribute : OrderedAttribute
    {
        public ValidationAttribute() { }
        public bool Required { get; set; }
        public abstract DataValidator Validator { get; }
    }
}