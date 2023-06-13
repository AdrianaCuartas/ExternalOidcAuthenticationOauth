using ExternalOidcAuthLibrary.Backend.Entities.Interfaces;
using ExternalOidcAuthLibrary.Backend.Entities.Interfaces.Endpoints;
using ExternalOidcAuthLibrary.Backend.Entities.ValueObjects;
using ExternalOidcAuthLibrary.Shared.Entities.Builders;
using ExternalOidcAuthLibrary.Shared.Entities.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Text.Json;

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

        var RequestBody = new RequestTokenBodyBuilder()
            .SetAuthorizationCode(code)
            .SetRedirectUri(ProviderInfo.RedirectUri)
            .SetClientId(ProviderInfo.ClientId)
            .SetScope("openid profile email")
            .SetCodeVerifier(CodeVerifier)
            .SetClientSecret(ProviderInfo.ClientSecret)
            .Build();
        HttpClient Client = HttpClientFactory.CreateClient();
        var Response = await Client.PostAsync(ProviderInfo.Token_Endpoint, RequestBody);

        var JsonElement = await Response.Content.ReadFromJsonAsync<JsonElement>();

        if (Response.IsSuccessStatusCode)
        {
            if (JsonElement.TryGetProperty("id_token", out JsonElement idTokenJson))
            {
                string idTokenToVerify = idTokenJson.ToString();
                var Handler = new JwtSecurityTokenHandler();

                var JwtToken = Handler.ReadJwtToken(idTokenToVerify);

                var IdTokenNonce = JwtToken.Claims
                    .FirstOrDefault(c => c.Type == "nonce")?.Value;
                if (IdTokenNonce != null && stateData.Nonce == IdTokenNonce)
                {
                    Tokens = new()
                    {
                        Id_Token = idTokenToVerify
                    };
                    if (JsonElement.TryGetProperty("access_token", out JsonElement accessTokenJson))
                    {
                        Tokens.Access_Token = accessTokenJson.ToString();
                    }
                }


            }
        }
        else
        {
            Logger.LogError(JsonElement.GetRawText());
        }
        return Tokens;

    }
}
