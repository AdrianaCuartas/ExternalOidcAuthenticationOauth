using ExternalOidcAuthLibrary.Shared.Entities.Dtos;
using ExternalOidcAuthLibrary.Shared.Entities.Interfaces;
using ExternalOidcAuthLibrary.Shared.Entities.ValueObjects;

namespace WebAPI.FakeServices;

public class UserProvider : IUserProvider
{
    public Tokens Login(ExternalUserDto userData) =>
        new Tokens()
        {
            Access_Token = userData.LastName,
            Id_Token = userData.Email,
            Refresh_Token = userData.FirstName
        };

    public Tokens Register(ExternalUserDto userData) =>
        new Tokens()
        {
            Access_Token = userData.LastName,
            Id_Token = userData.Email,
            Refresh_Token = userData.FirstName
        };
}
