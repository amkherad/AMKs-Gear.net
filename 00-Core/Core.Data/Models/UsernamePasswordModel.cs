using AMKsGear.Architecture.Data.Schema;

namespace AMKsGear.Core.Data.Models
{
    public class UsernameStringPasswordStringModel : IUsernameStringPassswordStringModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}