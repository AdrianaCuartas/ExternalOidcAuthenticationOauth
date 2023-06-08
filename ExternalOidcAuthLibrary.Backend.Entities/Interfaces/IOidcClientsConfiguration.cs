namespace ExternalOidcAuthLibrary.Backend.Entities.Interfaces;

public interface IOidcClientsConfiguration
{
    Task<ClientConfiguration> GetClientConfigurationAsync(string clientId);
}
