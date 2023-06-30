using ExternalOidcAuthLibrary.Blazor.UI.Services;
using ExternalOidcAuthLibrary.Shared.Entities.Interfaces;

namespace ExternalOidcAuthLibrary.Blazor.UI
{
    public static class DependendencyContainer
    {
        public static IServiceCollection AddExternalOidcAuthLibraryBlazorUIServices(
            this IServiceCollection services)
        {

            services.AddScoped<IAppStateService, AppStateService>();

            services.AddHttpClient(nameof(IRequestTokenService));
            services.AddScoped<IRequestTokenService, RequestTokenService>();

            return services;
        }
    }
}