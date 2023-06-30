namespace ExternalOidcAuthLibrary.Backend.EndpointsServices;

internal class AuthorizationEndpointService : IAuthorizationEndpointService
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

            if (Client != null && Client.RedirectUri == RedirectUri)
            {
                string ScopeProvider = Scope.Substring(Scope.IndexOf("_") + 1);
                Provider IdProvider = (Provider)Enum.Parse(typeof(Provider), ScopeProvider);
                var ProviderConfiguration = await Providers.GetProviderAsync(IdProvider);

                var Builder = new AuthorizationRequestBuilder(ProviderConfiguration.Authorization_Endpoint)
                    .SetClientId(ProviderConfiguration.ClientId)
                    .SetRedirectUri(ProviderConfiguration.RedirectUri)
                    .SetScope("openid profile email");

                if (ProviderConfiguration.SupportS256ChallengeMethod)
                {
                    Builder.SetCodeChallegeS256();
                }
                else
                {
                    Builder.SetCodeChallegePlain();
                }

                RequestState Data = new()
                {
                    CodeVerifier = Builder.CodeVerifier,
                    Nonce = Builder.Nonce,
                    Provider = IdProvider,
                    ClientData = new RequestStateClientData()
                    {
                        ClientId = ClientId,
                        RedirectUri = RedirectUri,
                        Scope = Scope,
                        State = State,
                        CodeChallenge = CodeChallenge,
                        Nonce = Nonce
                    }
                };
                await ApiStateService.SetItemAsync(Builder.State, Data);
                string UrlRedirect = Builder.Builder();
                Result = Results.Redirect(UrlRedirect);
            }
            else
            {
                Result = Results.BadRequest(ErrorResponses.InvalidClient);
            }

        }
        else
        {
            Result = Results.BadRequest(ErrorResponses.InvalidRequest);
        }
        return Result;
    }
}