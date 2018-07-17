namespace AMKsGear.Core.Data.Validation.Validators
{
    public abstract class DataValidator : IDataValidator
    {
        public virtual ILocalizableResult ErrorMessage { get; set; }

        public abstract bool Validate(object value, out ILocalizableResult result);
        public virtual bool Validate(object value, out string error)
        {
            ILocalizableResult errorResult;
            var result = Validate(value, out errorResult);
            error = errorResult.GetResult();
            return result;
        }
        public virtual bool IsValid(object value)
        {
            ILocalizableResult result;
            return Validate(value, out result);
        }
        public virtual string GetMessageFor(object value)
        {
            ILocalizableResult error;
            return (Validate(value, out error)
                ? Localization.Format<IValidationLocalization, DefaultValidationLocalization>(x => x.ValidValueString)
                : error).GetResult();
        }
        
        private static readonly DefaultDataValidator DefaultDataValidator = new DefaultDataValidator();
        public static DataValidator Default => DefaultDataValidator;
    }
}