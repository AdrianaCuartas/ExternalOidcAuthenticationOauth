using ExternalOidcAuthLibrary.Backend.MemoryProviders.Options;

namespace ExternalOidcAuthLibrary.Backend.MemoryProviders
{
    public static class DependendencyContainer
    {
        public static IServiceCollection AddExternalOidcAuthLibraryMemoryProvidersServices(
            this IServiceCollection services,
            Action<Dictionary<Provider, ProviderConfiguration>> providersSetter)
        {

            services.TryAddSingleton<IOidcProvidersConfiguration, OidcProvidersConfiguration>();

            services.Configure<ProviderConfigurationOptions>(options =>
             providersSetter(options.Providers));
            return services;
        }
    }
}