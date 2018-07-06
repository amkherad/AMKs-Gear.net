using AMKsGear.Architecture.Modeling;

namespace AMKsGear.Architecture.Data.Schema
{
    public interface IContactInfo :
        IFullNameString,
        IEmailString,
        IAddressString,
        IPhoneString,
        IDescriptionString
    { }

    public interface IContactInfoStringStringStringStringStringEntity :
        IContactInfo,
        IEntity,
        IFullNameStringEntity,
        IEmailStringEntity,
        IAddressStringEntity,
        IPhoneStringEntity,
        IDescriptionStringEntity
    { }
    public interface IContactInfoStringStringStringStringStringModel :
        IContactInfo,
        IModel,
        IFullNameStringModel,
        IEmailStringModel,
        IAddressStringModel,
        IPhoneStringModel,
        IDescriptionStringModel
    { }
}