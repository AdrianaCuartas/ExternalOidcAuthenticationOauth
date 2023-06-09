using ExternalOidcAuthLibrary.Shared.Entities.Constants;
namespace ExternalOidcAuthLibrary.Shared.Entities.Builders;
public class RequestTokenBodyBuilder
{

    const string GrantType = "authorization_code";

    //fields

    string Code;
    string RedirectUri;
    string ClientId;
    string Scope;
    string CodeVerifier;
    string ClientSecret;

    //setters

    public RequestTokenBodyBuilder SetAuthorizationCode(string code)
    {
        Code = code;
        return this;
    }

    public RequestTokenBodyBuilder SetRedirectUri(string redirectUri)
    {
        RedirectUri = redirectUri;
        return this;
    }

    public RequestTokenBodyBuilder SetClientId(string cilientId)
    {
        ClientId = cilientId;
        return this;
    }

    public RequestTokenBodyBuilder SetScope(string scope)
    {
        Scope = scope;
        return this;
    }

    public RequestTokenBodyBuilder SetCodeVerifier(string codeVerifier)
    {
        CodeVerifier = codeVerifier;
        return this;
    }

    public RequestTokenBodyBuilder SetClientSecret(string clientSecret)
    {
        ClientSecret = clientSecret;
        return this;
    }

    public FormUrlEncodedContent Build()
    {
        Dictionary<string, string> BodyData = new()
        {
            { RequestToken.Grant_Type, GrantType  },
            { RequestToken.Code, Code },
            { RequestToken.Redirect_Uri, RedirectUri },
            { RequestToken.Client_Id, ClientId},
            { RequestToken.Scope, Scope },
            { RequestToken.Code_Verifier, CodeVerifier },
            { RequestToken.Client_Secret,ClientSecret }

        };

        return new FormUrlEncodedContent(BodyData);
    }
}
