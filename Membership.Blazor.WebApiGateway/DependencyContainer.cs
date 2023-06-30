using HttpMessageHandlers;

namespace Membership.Blazor.WebApiGateway;

public static class DependendencyContainer
{
    public static IServiceCollection AddWebApiGatewayServices(
        this IServiceCollection services,
        Action<UserEndpointsOptions> userEndpointsOptionsSetter)
    {

        services.AddOptions<UserEndpointsOptions>().Configure(userEndpointsOptionsSetter);

        //forma 1 de inyectar
        services.AddHttpClient(nameof(IUserWebApiGateway)).AddHttpMessageHandler(() => new ExceptionDelegatingHandler());
        services.AddScoped<IUserWebApiGateway, UserWebApiGateway>();

        //forma 2 de inyectar
        //services.AddHttpClient<IUserWebApiGateway, UserWebApiGateway>(nameof(IUserWebApiGateway)).
        //    AddHttpMessageHandler(() => new ExceptionDelegatingHandler());


        //services.Configure<UserEndpointsOptions>(options => userEndpointsOptionsSetter(options));
        return services;
    }
}