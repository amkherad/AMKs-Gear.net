using AMKsGear.Core.Localization;

namespace AMKsGear.Core.Data.Validation.Validators
{
    public interface IDataValidator
    {
        bool Validate(object value, out ITranslateResult result);
        bool Validate(object value, out string error);
        bool IsValid(object value);
        string GetMessageFor(object value);
    }
}