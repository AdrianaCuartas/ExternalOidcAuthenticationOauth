using Microsoft.AspNetCore.Http;

namespace ExternalOidcAuthLibrary.Backend.Entities.Interfaces.Endpoints;

public interface IAuthorizationCallbackEndpointService
{
    Task<IResult> HandleAuthorizacionCodeAsync(HttpContext context);
}
