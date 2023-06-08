using ExternalOidcAuthLibrary.Backend.Entities.Interfaces;
using ExternalOidcAuthLibrary.Backend.Entities.Interfaces.Endpoints;
using ExternalOidcAuthLibrary.Shared.Entities.Constants;
using Microsoft.AspNetCore.Http;

namespace ExternalOidcAuthLibrary.Backend.EndpointsServices;

internal class AuthorizationEndpointService : IAuthorizationEndpoinstService
{
    readonly IOidcProvidersConfiguration Providers;
    readonly IOidcClientsConfiguration Clients;
    readonly IApiStateService ApiStateService;

    public AuthorizationEndpointService(IOidcProvidersConfiguration providers,
        IOidcClientsConfiguration clients, IApiStateService apiStateService)
    {
        Providers = providers;
        Clients = clients;
        ApiStateService = apiStateService;
    }

    public async Task<IResult> AuthorizeAsync(HttpContext context)
    {
        IResult Result;

        string ClientId = context.Request.Query[RequestAuthorizationCode.ClientId];

        string RedirectUri = context.Request.Query[RequestAuthorizationCode.RedirectUri];

        string Scope = context.Request.Query[RequestAuthorizationCode.Scope];

        string State = context.Request.Query[RequestAuthorizationCode.State];

        string CodeChallenge = context.Request.Query[RequestAuthorizationCode.CodeChallenge];

        string CodeChallengeMethod = context.Request.Query[RequestAuthorizationCode.CodeChallengeMethod];

        string Nonce = context.Request.Query[RequestAuthorizationCode.Nonce];


        //se debe validar con string.IsNullOrWhiteSpace
        if (ClientId != null && RedirectUri != null && Scope != null && State != null
            && CodeChallenge != null && CodeChallengeMethod != null && Nonce != null)
        {
            var Client = await Clients.GetClientConfigurationAsync(ClientId);


        }
        return Result;
    }
}