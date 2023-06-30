namespace ExternalOidcAuthLibrary.Backend.EndpointsServices;

internal class AuthorizationTokenEndpointService : IAuthorizationTokenEndpointService
{
    readonly IApiStateService ApiStateService;
    readonly IUserProvider UserProvider;
    public AuthorizationTokenEndpointService(IApiStateService apiStateService,
        IUserProvider userProvider)
    {
        ApiStateService = apiStateService;

        UserProvider = userProvider;
    }

    public async Task<IResult> GetTokensAsync(HttpContext context)
    {
        IResult Result = Results.BadRequest(ErrorResponses.InvalidRequest);

        var Code = context.Request.Form[RequestToken.Code];

        string GrantType = context.Request.Form[RequestToken.Grant_Type];

        string RedirectUri = context.Request.Form[RequestToken.Redirect_Uri];

        string ClientId = context.Request.Form[RequestToken.Client_Id];

        string Scope = context.Request.Form[RequestToken.Scope];

        string CodeVerifier = context.Request.Form[RequestToken.Code_Verifier];

        var StateData = await ApiStateService.GetItemStateAsync(Code);
        if (StateData != null)
        {
            string ScopeProvider = Scope.Substring(Scope.IndexOf("_") + 1);
            Provider IdProvider = (Provider)Enum.Parse(typeof(Provider), ScopeProvider);

            var ClientData = StateData.ClientData;

            //validar el proveedor ?
            if (GrantType == "authorization_code" && RedirectUri == ClientData.RedirectUri &&
                ClientId == ClientData.ClientId && Scope == ClientData.Scope &&
               PKCE.GetHash256CodeChallenge(CodeVerifier) == ClientData.CodeChallenge)
            {
                Tokens Tokens = GetTokens(ClientData.Scope, ClientData.Tokens.Id_Token, IdProvider, ClientData.Nonce);
                Result = Results.Ok(Tokens);

            }
        }
        return Result;
    }

    private Tokens GetTokens(string scope, string id_Token, Provider idProvider, string nonce)
    {
        Tokens Result = null;

        string ScopeActionFound = scope.Substring(0, scope.IndexOf("_"));
        ScopeAction ScopeUsed = (ScopeAction)Enum.Parse(typeof(ScopeAction), ScopeActionFound);

        var UserData = GetUserExternal(id_Token, idProvider);

        if (ScopeUsed == ScopeAction.Login)
            Result = UserProvider.Login(UserData);
        else
            Result = UserProvider.Register(UserData);

        return Result;
    }

    private ExternalUserDto GetUserExternal(string id_Token, Provider idProvider)
    {
        //Obtener  los datos del usuario apartir de IdToken:
        var UserData = GetUserFromIDToken(id_Token);
        UserData.Provider = idProvider.ToString();

        return UserData;
    }

    ExternalUserDto GetUserFromIDToken(string IdToken)
    {
        ExternalUserDto UserData = new();
        var Handler = new JwtSecurityTokenHandler();
        var Token = Handler.ReadJwtToken(IdToken);

        UserData.Sub = GetValue(Token, "sub");
        UserData.FirstName = GetValue(Token, "given_name");
        UserData.LastName = GetValue(Token, "family_name");
        UserData.Email = GetValue(Token, "email");
        return UserData;
    }

    string GetValue(JwtSecurityToken token, string claim)
    {
        string result;
        try
        {
            result = token.Claims.Where(c => c.Type == claim).Select(c => c.Value).FirstOrDefault();
        }
        catch (Exception)
        {

            result = string.Empty;
        }
        return result;
    }
}

