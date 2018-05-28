using AMKsGear.Architecture.Data.Schema;

namespace AMKsGear.Core.Data
{
    public static class SchemaExtensions
    {
        public static string GetFullName(this IFullName user)
        {
            return $"{user.FirstName} {user.LastName}";
        }
    }
}