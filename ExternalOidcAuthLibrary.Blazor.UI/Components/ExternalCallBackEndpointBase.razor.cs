using ExternalOidcAuthLibrary.Shared.Entities.Interfaces;
using ExternalOidcAuthLibrary.Shared.Entities.ValueObjects;

namespace ExternalOidcAuthLibrary.Blazor.UI.Components;

public partial class ExternalCallBackEndpointBase : ComponentBase
{
    #region Services
    [Inject]
    IOptions<AppOptions> AppOptions { get; set; }

    [Inject]
    IAppStateService AppStateService { get; set; }

    [Inject]
    IRequestTokenService RequestTokenService { get; set; }


    [Inject]
    IAuthenticationCallback AuthenticationCallback { get; set; }
    #endregion

    [Parameter]
    [SupplyParameterFromQuery]
    public string State { get; set; }

    [Parameter]
    [SupplyParameterFromQuery]
    public string Code { get; set; }

    protected String IdToken { get; set; }
    protected String AccessToken { get; set; }
    protected async override Task OnParametersSetAsync()
    {
        if (Code != null && State != null)
        {
            string OriginalState =
             await AppStateService.GetItemAsync<string>(StateKeys.State);
            if (OriginalState == State)
            {
                string CodeVerifier
                    = await AppStateService.GetItemAsync<string>(StateKeys.CodeVerifier);

                string Scope
                   = await AppStateService.GetItemAsync<string>(StateKeys.Scope);

                string Nonce
                   = await AppStateService.GetItemAsync<string>(StateKeys.Nonce);

                string TokenEndpoint
                  = AppOptions.Value.Token_Endpoint;

                var Tokens = await RequestTokenService.PostAsync(new
                    RequestTokenParameters(TokenEndpoint, Code,
                    AppOptions.Value.Redirect_Uri,
                    AppOptions.Value.Client_Id, Scope, CodeVerifier, Nonce));
                if (Tokens != null)
                {
                    IdToken = Tokens.Id_Token;
                    AccessToken = Tokens.Access_Token;
                    await AuthenticationCallback.Authentication(Tokens);


                }

            }
        }
    }




}
