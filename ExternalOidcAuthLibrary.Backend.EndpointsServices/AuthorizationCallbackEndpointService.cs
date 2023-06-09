using ExternalOidcAuthLibrary.Backend.Entities.Interfaces;
using ExternalOidcAuthLibrary.Backend.Entities.Interfaces.Endpoints;
using ExternalOidcAuthLibrary.Backend.Entities.ValueObjects;
using ExternalOidcAuthLibrary.Shared.Entities.Builders;
using ExternalOidcAuthLibrary.Shared.Entities.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ExternalOidcAuthLibrary.Backend.EndpointsServices;

internal class AuthorizationCallbackEndpointService : IAuthorizationCallbackEndpointService
{
    readonly IApiStateService ApiStateService;
    readonly IOidcProvidersConfiguration Providers;
    readonly ILogger<AuthorizationCallbackEndpointService> Logger;
    readonly IHttpClientFactory HttpClientFactory;

    public AuthorizationCallbackEndpointService(IApiStateService apiStateService,
        IOidcProvidersConfiguration providers, ILogger<AuthorizationCallbackEndpointService> logger,
        IHttpClientFactory httpClientFactory)
    {
        ApiStateService = apiStateService;
        Providers = providers;
        Logger = logger;
        HttpClientFactory = httpClientFactory;
    }

    async Task<IResult> IAuthorizationCallbackEndpointService.HandleAuthorizacionCodeAsync(HttpContext context)
    {
        IResult Result = Results.BadRequest(ErrorResponses.InvalidState);

        string Code = context.Request.Query["code"];
        string State = context.Request.Query["state"]; //va a a acceder los datos en el estado

        var StateData = await ApiStateService.GetItemStateAsync(State);

        if (StateData != null)
        {
            Tokens Tokens = await GetTokensAsync(StateData, Code);
            if (Tokens != null)
            {
                StateData.ClientData.Tokens = Tokens;
                string Url = $"{StateData.ClientData.RedirectUri}?" +
                    $"state={StateData.ClientData.State}&code={State}";

                Result = Results.Redirect(Url);
            }
            else
            {
                Result = Results.BadRequest(ErrorResponses.UnableToGetExternalTokens);
            }
        }
        return Result;
    }

    private async Task<Tokens> GetTokensAsync(RequestState stateData, string code)
    {
        Tokens Tokens = null;
        var CodeVerifier = stateData.CodeVerifier;
        var Provider = stateData.Provider;

        var ProviderInfo = await Providers.GetProviderAsync(Provider);

        //Aqui vamos......
        var RequestBody = new RequestTokenBodyBuilder();
        return Tokens;

    }
}
