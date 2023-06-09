using ExternalOidcAuthLibrary.Backend.MemoryClients.Options;

namespace ExternalOidcAuthLibrary.Backend.MemoryClients;

public class OidcClientsConfiguration : IOidcClientsConfiguration
{
    readonly ClientConfigurationOptions ClientsOptions;
    public OidcClientsConfiguration(IOptions<ClientConfigurationOptions> clientsOptions)
    {
        ClientsOptions = clientsOptions.Value;
    }
    public async Task<ClientConfiguration> GetClientConfigurationAsync(string clientId)
    {

        return await Task.FromResult(ClientsOptions[clientId]);
    }
}