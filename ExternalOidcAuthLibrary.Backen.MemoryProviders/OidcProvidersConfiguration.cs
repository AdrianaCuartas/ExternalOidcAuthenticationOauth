using ExternalOidcAuthLibrary.Backend.MemoryProviders.Options;

namespace ExternalOidcAuthLibrary.Backend.MemoryProviders
{
    public class OidcProvidersConfiguration : IOidcProvidersConfiguration
    {
        readonly ProviderConfigurationOptions ProvidersOptions;
        public OidcProvidersConfiguration(IOptions<ProviderConfigurationOptions> providersOptions)
        {
            ProvidersOptions = providersOptions.Value;
        }


        public async Task<ProviderConfiguration> GetProviderAsync(Provider provider)
        {
            return await Task.FromResult(ProvidersOptions[provider]);
        }
    }
}