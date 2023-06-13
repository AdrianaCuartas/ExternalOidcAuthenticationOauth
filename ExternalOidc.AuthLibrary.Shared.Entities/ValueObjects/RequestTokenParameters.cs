namespace ExternalOidcAuthLibrary.Shared.Entities.ValueObjects;

public class RequestTokenParameters
{
    public string TokenEndpoint { get; set; }

    public string AuthorizatonCode { get; set; }

    public string RedirectUri { get; set; }

    public string ClientId { get; set; }

    public string Scope { get; set; }

    public string CodeVerifier { get; set; }

    public string Nonce { get; set; }

    public string ClientSecret { get; set; }

    public RequestTokenParameters(string tokenEndpoint, string authorizatonCode, string redirectUri,
        string clientId, string scope, string codeVerifier, string nonce, string clientSecret = null)
    {
        TokenEndpoint = tokenEndpoint;
        AuthorizatonCode = authorizatonCode;
        RedirectUri = redirectUri;
        ClientId = clientId;
        Scope = scope;
        CodeVerifier = codeVerifier;
        Nonce = nonce;
        ClientSecret = clientSecret;
    }
}
