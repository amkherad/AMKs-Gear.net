namespace AMKsGear.Core.Data.Validation.Validators
{
    public class EmailDataValidator : StringDataValidator
    {
        public static readonly string EmailRegexPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

        public EmailDataValidator()
            : base(EmailRegexPattern) { }
        public EmailDataValidator(int minLength)
            : base(minLength, int.MaxValue, EmailRegexPattern) { }
        public EmailDataValidator(int minLength, int maxLength)
            : base(minLength, maxLength, EmailRegexPattern) { }

        public static bool Validate(string email) => ValidateRegexPattern(EmailRegexPattern, email?.ToLower());
    }
}