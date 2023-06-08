using ExternalOidcAuthLibrary.Shared.Entities.Builders;

namespace ExternalOidcAuthLibrary.Blazor.UI.Components;
public partial class ExternalProviderButtons
{

    #region Services

    [Inject]
    IOptions<AppOptions> AppOptions { get; set; }


    [Inject]
    IAppStateService AppStateService { get; set; }

    [Inject]
    NavigationManager NavigationManager { get; set; }

    #endregion


    #region Parameters
    [Parameter]
    public RenderFragment<string> ButtonContent { get; set; }


    [Parameter]
    public ScopeAction ScopeAction { get; set; } = ScopeAction.Login;

    [Parameter(CaptureUnmatchedValues = true)]
    //el contendor de los botones definidos:
    public Dictionary<string, object> Attributes { get; set; }
    #endregion


    #region Private fields and properties

    ProviderInfo[] Providers;

    //ruta de las imagenes
    string ContentPath =>
        $"_content/{this.GetType().Assembly.GetName().Name}";


    #endregion


    #region Methods
    //si la ruta del proveedor no fue especificada: se busca la imagen del proyecto, o si
    //viene la imagen se tendra encuenta
    string ImagePath(ProviderInfo provider) =>
        string.IsNullOrWhiteSpace(provider.ImagePath) ?
        $"{ContentPath}/images/providers/{provider.Provider}.png" :
        provider.ImagePath;

    protected override void OnInitialized()
    {
        Providers = AppOptions.Value.Providers;
    }

    async void BuildUrl(string provider)
    {
        var Builder = new AuthorizationRequestBuilder(
            AppOptions.Value.Authorization_Endpoint)
            .SetClientId(AppOptions.Value.ClientId)
            .SetRedirectUri(AppOptions.Value.Redirect_Uri)
            .SetScope($"{ScopeAction}_{provider}")
            .SetCodeChallegeS256();

        await AppStateService.SetItemAsync(StateKeys.State, Builder.State);
        await AppStateService.SetItemAsync(StateKeys.CodeVerifier, Builder.CodeVerifier);
        await AppStateService.SetItemAsync(StateKeys.Nonce, Builder.Nonce);
        await AppStateService.SetItemAsync(StateKeys.Scope, Builder.Scope);

        //fuerza la recarga con el segundo parametro:
        NavigationManager.NavigateTo(Builder.Builder(), true);

    }
    #endregion

}
