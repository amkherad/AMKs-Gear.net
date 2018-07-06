using AMKsGear.Architecture.Data.Schema;

namespace AMKsGear.Core.Data
{
    public static class SchemaExtensions
    {
        public static string GetFullName(this IFullNameString user)
        {
            return $"{user.FirstName} {user.LastName}";
        }
    }
}