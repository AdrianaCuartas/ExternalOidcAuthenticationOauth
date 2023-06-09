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
            services.TryAddSingleton<IAuthorizationEndpoinstService, AuthorizationEndpointService>();

            return services;
        }
    }
}