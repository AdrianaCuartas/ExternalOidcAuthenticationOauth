using ExternalOidcAuthLibrary.Backend.MemoryClients.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ExternalOidcAuthLibrary.Backend.MemoryClients;

public static class DependendencyContainer
{
    public static IServiceCollection AddExternalOidcAuthLibraryMemoryClientServices(
        this IServiceCollection services, Action<Dictionary<string, ClientConfiguration>> clientsSetter)
    {

        services.TryAddSingleton<IOidcClientsConfiguration, OidcClientsConfiguration>();
        services.Configure<ClientConfigurationOptions>(options => clientsSetter(options.Clients));
        return services;
    }
}