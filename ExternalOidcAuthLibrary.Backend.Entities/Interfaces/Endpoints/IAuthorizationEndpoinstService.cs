
using Microsoft.AspNetCore.Http;

namespace ExternalOidcAuthLibrary.Backend.Entities.Interfaces.Endpoints;

public interface IAuthorizationEndpoinstService
{
    Task<IResult> AuthorizeAsync(HttpContext context);
}
