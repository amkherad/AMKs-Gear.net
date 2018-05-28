using System;

namespace AMKsGear.Core.Security
{
    public interface IToken
    {
        DateTime CreatedDateTime { get; set; }
        DateTime? UpdateDateTime { get; set; }

        DateTime ExpirationDateTime { get; set; }
    }
    public interface IRoleToken : IToken
    {
        string Roles { get; set; }
    }
    public interface IUserToken<TUserId> : IToken
        //where TUserId : struct, IEquatable<TUserId>
    {
        TUserId UserId { get; set; }
    }
    public interface IInt32UserToken : IUserToken<int>, IToken { }
    public interface IInt64UserToken : IUserToken<long>, IToken { }
    public interface IGuidUserToken : IUserToken<Guid>, IToken { }

    public interface IInt32UserRoleToken : IRoleToken, IInt32UserToken, IToken { }
    public interface IInt64UserRoleToken : IRoleToken, IInt64UserToken, IToken { }
    public interface IGuidUserRoleToken : IRoleToken, IGuidUserToken, IToken { }
}
