using Microsoft.AspNetCore.Http;

namespace ExternalOidcAuthLibrary.Backend.Entities.Interfaces.Endpoints;

public interface IAuthorizationTokenEndpointService
{
    Task<IResult> GetTokensAsync(HttpContext context);
}
