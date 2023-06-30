using ExternalOidcAuthLibrary.Shared.Entities.Builders;
using ExternalOidcAuthLibrary.Shared.Entities.Interfaces;
using ExternalOidcAuthLibrary.Shared.Entities.ValueObjects;
using System.Net.Http.Json;

namespace ExternalOidcAuthLibrary.Blazor.UI.Services;

internal class RequestTokenService : IRequestTokenService
{

    readonly HttpClient HttpClient;

    public RequestTokenService(IHttpClientFactory factory)
    {
        HttpClient = factory.CreateClient(nameof(IRequestTokenService));
    }

    public string ResponseError { get; private set; }

    public async Task<Tokens> PostAsync(RequestTokenParameters parameters)
    {
        Tokens Tokens = null;

        var RequestBody = new RequestTokenBodyBuilder()
           .SetAuthorizationCode(parameters.AuthorizatonCode)
           .SetRedirectUri(parameters.RedirectUri)
           .SetClientId(parameters.ClientId)
           .SetScope(parameters.Scope)
           .SetCodeVerifier(parameters.CodeVerifier)
           .Build();

        var ResponseMessage = await HttpClient.PostAsync(parameters.TokenEndpoint, RequestBody);

        if (ResponseMessage.IsSuccessStatusCode)
        {

            Tokens = await ResponseMessage.Content.ReadFromJsonAsync<Tokens>();
        }
        else
        {
            ResponseError = ResponseMessage.ReasonPhrase;
        }
        return Tokens;
    }
}
