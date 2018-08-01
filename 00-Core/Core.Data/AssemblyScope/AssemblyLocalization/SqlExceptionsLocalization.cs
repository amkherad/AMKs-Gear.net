using AMKsGear.Architecture.Localization;

namespace AMKsGear.Core.Data.AssemblyScope.AssemblyLocalization
{
    public interface ISqlExceptionsLocalization : ILocalizationModel
    {
        string ForeignKeyViolation { get; }
        string InvalidDatabase { get; }
        string LoginFailed { get; }
        string ConstraintViolation { get; }
        string UniqueIndexOrConstraintViolation { get; }
        string UniqueIndexOrPrimaryKeyOrConstraintViolation { get; }
    }
    public class DefaultSqlExceptionsLocalization : DefaultEnglishLocalization, ISqlExceptionsLocalization
    {
        public const string DefaultForeignKeyViolation = "Foreign key violation exception has been occurred.";
        public const string DefaultInvalidDatabase = "Invalid database exception has been occured.";
        public const string DefaultLoginFailed = "Database login failed.";
        public const string DefaultConstraintViolation = "Unique index or constriant violation exception has been occurred.";
        public const string DefaultUniqueIndexOrConstraintViolation = "Unique index or constriant violation exception has been occurred.";
        public const string DefaultUniqueIndexOrPrimaryKeyOrConstraintViolation = "Unique index or primary key or constriant violation exception has been occurred.";
        

        public string ForeignKeyViolation => DefaultForeignKeyViolation;
        public string InvalidDatabase => DefaultInvalidDatabase;
        public string LoginFailed => DefaultLoginFailed;
        public string ConstraintViolation => DefaultUniqueIndexOrConstraintViolation;
        public string UniqueIndexOrConstraintViolation => DefaultUniqueIndexOrConstraintViolation;
        public string UniqueIndexOrPrimaryKeyOrConstraintViolation => DefaultUniqueIndexOrPrimaryKeyOrConstraintViolation;
    }
}