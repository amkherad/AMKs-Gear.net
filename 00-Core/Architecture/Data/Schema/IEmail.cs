using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Data.Schema
{
    public interface IEmail
    {
        string Email { get; set; }
    }
    public interface IEmailEntity : IEmail, IEntity { }
    public interface IEmailModel : IEmail, IModel { }
}