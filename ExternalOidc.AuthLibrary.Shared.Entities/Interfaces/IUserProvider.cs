using ExternalOidcAuthLibrary.Shared.Entities.Dtos;
using ExternalOidcAuthLibrary.Shared.Entities.ValueObjects;

namespace ExternalOidcAuthLibrary.Shared.Entities.Interfaces;

public interface IUserProvider
{
    Tokens Login(ExternalUserDto userData);

    Tokens Register(ExternalUserDto userData);
}
