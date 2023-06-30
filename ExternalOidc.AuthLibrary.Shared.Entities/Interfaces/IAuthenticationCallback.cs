using ExternalOidcAuthLibrary.Shared.Entities.ValueObjects;

namespace ExternalOidcAuthLibrary.Shared.Entities.Interfaces;

public interface IAuthenticationCallback
{
    //aqui se debe persistir el usuario:
    Task Authentication(Tokens tokens);
}
