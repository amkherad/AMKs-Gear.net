using AMKsGear.Architecture.LocalizationFramework;

namespace AMKsGear.Core.Data.AssemblyScope.AssemblyLocalization
{
    public interface ISqlExceptionsLocalization : ILocalization
    {
        string ForeignKeyViolation { get; }
        string InvalidDatabase { get; }
        string LoginFailed { get; }
        string UniqueIndexOrConstriantViolation { get; }
        string UniqueIndexOrPrimaryKeyOrConstriantViolation { get; }
    }
    public class DefaultSqlExceptionsLocalization : ISqlExceptionsLocalization
    {
        public const string DefaultForeignKeyViolation = "Foreign key violation exception has been occurred.";
        public const string DefaultInvalidDatabase = "Invalid database exception has been occured.";
        public const string DefaultLoginFailed = "Database login failed.";
        public const string DefaultUniqueIndexOrConstriantViolation = "Unique index or constriant violation exception has been occurred.";
        public const string DefaultUniqueIndexOrPrimaryKeyOrConstriantViolation = "Unique index or primary key or constriant violation exception has been occurred.";
        

        public string ForeignKeyViolation => DefaultForeignKeyViolation;
        public string InvalidDatabase => DefaultInvalidDatabase;
        public string LoginFailed => DefaultLoginFailed;
        public string UniqueIndexOrConstriantViolation => DefaultUniqueIndexOrConstriantViolation;
        public string UniqueIndexOrPrimaryKeyOrConstriantViolation => DefaultUniqueIndexOrPrimaryKeyOrConstriantViolation;
    }
}