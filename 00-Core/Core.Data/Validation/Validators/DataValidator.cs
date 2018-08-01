using AMKsGear.Core.Localization;

namespace AMKsGear.Core.Data.Validation.Validators
{
    public abstract class DataValidator : IDataValidator
    {
        public virtual ITranslateResult ErrorMessage { get; set; }

        public abstract bool Validate(object value, out ITranslateResult result);
        public virtual bool Validate(object value, out string error)
        {
            var result = Validate(value, out ITranslateResult errorResult);
            error = errorResult.GetValue();
            return result;
        }
        public virtual bool IsValid(object value)
        {
            ITranslateResult result;
            return Validate(value, out result);
        }
        public virtual string GetMessageFor(object value)
        {
            return (Validate(value, out ITranslateResult error)
                ? LocalizationServices.Format<IValidationLocalization, DefaultValidationLocalization>(x => x.ValidValueString)
                : error.GetValue());
        }
        
        private static readonly DefaultDataValidator DefaultDataValidator = new DefaultDataValidator();
        public static DataValidator Default => DefaultDataValidator;
    }
}