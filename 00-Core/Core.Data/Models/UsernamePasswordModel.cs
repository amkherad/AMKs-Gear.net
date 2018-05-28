using AMKsGear.Architecture.Data.Schema;

namespace AMKsGear.Core.Data.Models
{
    public class UsernamePasswordModel : IUsernamePassswordModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}