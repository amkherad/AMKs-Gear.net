using AMKsGear.Architecture.Modeling;
using AMKsGear.Core.Data.Validation.Validators;

namespace AMKsGear.Core.Data.Validation
{
    public static class ValidationExtensions
    {
        public static IDataValidator GetValidator(this object @object)
        {
            return null;
        }
        public static IDataValidator GetValidator(this IModel model)
        {
            return null;
        }
    }
}