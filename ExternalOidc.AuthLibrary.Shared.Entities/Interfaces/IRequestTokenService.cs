using ExternalOidcAuthLibrary.Shared.Entities.ValueObjects;

namespace ExternalOidcAuthLibrary.Shared.Entities.Interfaces;

public interface IRequestTokenService
{
    Task<Tokens> PostAsync(RequestTokenParameters parameters);

    string ResponseError { get; }
}
