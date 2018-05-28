using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Data.Schema
{
    public interface IContactInfo :
        IFullName,
        IEmail,
        IAddress,
        IPhone,
        IDescription
    { }

    public interface IContactInfoEntity :
        IContactInfo,
        IEntity,
        IFullNameEntity,
        IEmailEntity,
        IAddressEntity,
        IPhoneEntity,
        IDescriptionEntity
    { }
    public interface IContactInfoModel :
        IContactInfo,
        IModel,
        IFullNameModel,
        IEmailModel,
        IAddressModel,
        IPhoneModel,
        IDescriptionModel
    { }
}