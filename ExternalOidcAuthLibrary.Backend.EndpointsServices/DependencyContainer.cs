using ExternalOidcAuthLibrary.Backend.Entities.Interfaces.Endpoints;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ExternalOidcAuthLibrary.Backend.EndpointsServices
{
    public static class DependendencyContainer
    {
        public static IServiceCollection AddEndpointsServices(
            this IServiceCollection services)
        {
            services.TryAddSingleton<IAuthorizationEndpointService,
                AuthorizationEndpointService>();

            services.TryAddSingleton<IAuthorizationCallbackEndpointService,
                AuthorizationCallbackEndpointService>();


            services.TryAddSingleton<IAuthorizationTokenEndpointService,
                AuthorizationTokenEndpointService>();

            services.AddHttpClient();


            return services;
        }
    }
}