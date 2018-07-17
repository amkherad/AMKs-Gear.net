using AMKsGear.Core.Localization;

namespace AMKsGear.Core.Data.Validation.Validators
{
    public class DefaultDataValidator : DataValidator
    {
        public override bool Validate(object value, out ILocalizableResult result) => Validate(this, value, out result);
        public override bool Validate(object value, out string error)
        {
            ILocalizableResult errorResult;
            var result = Validate(this, value, out errorResult);
            error = errorResult.GetResult();
            return result;
        }

        public static bool Validate(DataValidator validator, object value, out ILocalizableResult error)
        {
            error = string.Empty.ToLocalizableResult();
            return false;
            //if (value == null)
            //{
            //    if (validator. && (validator.RequiredMode.RequiredValidationMode == RequiredValidationModes.InvalidOnDefaultValue))
            //    {
            //        error = string.Format(traslator.Translate(Resources.NullValue), validator.FieldName);
            //        return false;
            //    }
            //    error = traslator.Translate(Resources.ValidValue);
            //    return true;
            //}
            //if (validator.RequiredMode.IsRequired && (validator.RequiredMode.RequiredValidationMode == RequiredValidationModes.InvalidOnDefaultValue))
            //{
            //    Type t = value.GetType();
            //    if (t == typeof(string))
            //    {
            //        if (((string)value).Length == 0)
            //        {
            //            error = string.Format(traslator.Translate(Resources.NullValue), validator.FieldName);
            //            return false;
            //        }
            //    }
            //    else if (t == typeof(int))
            //    {
            //        if ((int)value == 0)
            //        {
            //            error = string.Format(traslator.Translate(Resources.NullValue), validator.FieldName);
            //            return false;
            //        }
            //    }
            //    else if (t == typeof(long))
            //    {
            //        if ((long)value == 0)
            //        {
            //            error = string.Format(traslator.Translate(Resources.NullValue), validator.FieldName);
            //            return false;
            //        }
            //    }
            //    else if (t == typeof(float))
            //    {
            //        if ((float)value == 0)
            //        {
            //            error = string.Format(traslator.Translate(Resources.NullValue), validator.FieldName);
            //            return false;
            //        }
            //    }
            //    else if (t == typeof(double))
            //    {
            //        if ((double)value == 0)
            //        {
            //            error = string.Format(traslator.Translate(Resources.NullValue), validator.FieldName);
            //            return false;
            //        }
            //    }
            //    else if (t == typeof(DateTime))
            //    {
            //        if ((DateTime)value == DateTime.MinValue)
            //        {
            //            error = string.Format(traslator.Translate(Resources.NullValue), validator.FieldName);
            //            return false;
            //        }
            //    }
            //}

            //error = traslator.Translate(Resources.ValidValue);
            //return true;
        }
    }
}
