using ExternalOidcAuthLibrary.Blazor.UI.Services;

namespace ExternalOidcAuthLibrary.Blazor.UI
{
    public static class DependendencyContainer
    {
        public static IServiceCollection AddExternalOidcAuthLibraryBlazorUIServices(
            this IServiceCollection services)
        {

            services.AddScoped<IAppStateService, AppStateService>();
            return services;
        }
    }
}