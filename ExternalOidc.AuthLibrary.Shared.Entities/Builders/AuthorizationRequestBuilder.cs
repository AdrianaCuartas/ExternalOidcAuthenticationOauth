using ExternalOidcAuthLibrary.Shared.Entities.Constants;

namespace ExternalOidcAuthLibrary.Shared.Entities.Builders;

public class AuthorizationRequestBuilder
{
    public AuthorizationRequestBuilder(string authorizationEndpoint)
    {
        AuthorizationEndpoint = authorizationEndpoint;
        State = Base64UrlEncoded.GetRandomString(32); //genera una cadena aleatoria de 32 caracteres
        Nonce = Base64UrlEncoded.GetRandomString(12);//genera una cadena aleatoria de 12 caracteres

    }

    #region Private fields
    string ResponseType = "code";
    string AuthorizationEndpoint;

    string ClientId;
    string RedirectUri;

    string CodeChallenge;
    string CodeChallengeMethod;

    #endregion

    #region Public Properties
    public string State { get; set; }
    public string Nonce { get; set; }
    public string CodeVerifier { get; private set; }

    public string Scope { get; set; }

    #endregion

    #region Setters Method

    public AuthorizationRequestBuilder SetResponseType(string responseType)
    {
        ResponseType = responseType;
        return this;
    }
    public AuthorizationRequestBuilder SetClientId(string clientId)
    {
        ClientId = clientId;
        return this;
    }
    public AuthorizationRequestBuilder SetRedirectUri(string redirectUri)
    {
        RedirectUri = redirectUri;
        return this;
    }
    public AuthorizationRequestBuilder SetScope(string scope)
    {
        Scope = scope;
        return this;
    }
    public AuthorizationRequestBuilder SetCodeChallegeS256()
    {
        CodeChallengeMethod = "S256";
        CodeVerifier = PKCE.GetCodeVerifier();
        CodeChallenge = PKCE.GetHash256CodeChallenge(CodeVerifier);
        return this;
    }
    public AuthorizationRequestBuilder SetCodeChallegePlain()
    {
        CodeChallengeMethod = "plain";
        CodeVerifier = PKCE.GetCodeVerifier();
        CodeChallenge = CodeVerifier;
        return this;
    }
    #endregion

    //Build
    public string Builder()
    {
        StringBuilder SB = new StringBuilder($"{AuthorizationEndpoint}?");
        SB.Append($"{RequestAuthorizationCode.ResponseType}={ResponseType}&");
        SB.Append($"{RequestAuthorizationCode.ClientId}={ClientId}&");
        SB.Append($"{RequestAuthorizationCode.RedirectUri}={RedirectUri}&");
        SB.Append($"{RequestAuthorizationCode.State}={State}&");
        SB.Append($"{RequestAuthorizationCode.Scope}={Scope}&");
        SB.Append($"{RequestAuthorizationCode.CodeChallenge}={CodeChallenge}&");
        SB.Append($"{RequestAuthorizationCode.CodeChallengeMethod}={CodeChallengeMethod}&");
        SB.Append($"{RequestAuthorizationCode.Nonce}={Nonce}");


        return SB.ToString();

    }
}
