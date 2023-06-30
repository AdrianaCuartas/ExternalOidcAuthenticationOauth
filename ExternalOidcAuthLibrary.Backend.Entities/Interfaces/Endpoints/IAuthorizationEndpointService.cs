
using Microsoft.AspNetCore.Http;

namespace ExternalOidcAuthLibrary.Backend.Entities.Interfaces.Endpoints;

public interface IAuthorizationEndpointService
{
    Task<IResult> AuthorizeAsync(HttpContext context);
}
