using ExternalOidcAuthLibrary.Backend.EndpointsServices;
using ExternalOidcAuthLibrary.Backend.Entities.Enums;
using ExternalOidcAuthLibrary.Backend.Entities.Interfaces.Endpoints;
using ExternalOidcAuthLibrary.Backend.Entities.ValueObjects;
using ExternalOidcAuthLibrary.Backend.MemoryClients;
using ExternalOidcAuthLibrary.Backend.MemoryProviders;
using ExternalOidcAutLibrary.Bakend.MemoryCacheState;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ExternalOidcAuthLibrary.Backend.IoC;

public static class DependendencyContainer
{
    public static IServiceCollection AddExternalOidcAuthLibraryServices(
        this IServiceCollection services,
        Action<Dictionary<string, ClientConfiguration>> clientsSetter,
         Action<Dictionary<Provider, ProviderConfiguration>> providerSetter

         )
    {

        services.AddMemoryCacheStateServices()
            .AddExternalOidcAuthLibraryMemoryClientServices(clientsSetter)
            .AddExternalOidcAuthLibraryMemoryProvidersServices(providerSetter)
            .AddEndpointsServices();


        return services;
    }

    public static WebApplication UseExternalOidcLibraryEndPoints(this WebApplication app)
    {

        app.MapGet(OidceMetadata.Authorization_Endpoint,
            async (HttpContext context, IAuthorizationEndpointService service) =>
            await service.AuthorizeAsync(context));

        app.MapGet(OidceMetadata.AuthorizationCallback_Endpoint,
            async (HttpContext context, IAuthorizationCallbackEndpointService service) =>
            await service.HandleAuthorizacionCodeAsync(context));

        app.MapPost(OidceMetadata.Token_Endpoint, async (HttpContext context,
            IAuthorizationTokenEndpointService service) =>
             await service.GetTokensAsync(context));

        return app;
    }
}