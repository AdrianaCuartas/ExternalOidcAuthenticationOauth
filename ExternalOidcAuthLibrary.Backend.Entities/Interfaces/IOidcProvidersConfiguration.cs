namespace ExternalOidcAuthLibrary.Backend.Entities.Interfaces;

public interface IOidcProvidersConfiguration
{
    Task<ProviderConfiguration> GetProviderAsync(Provider provider);

}
