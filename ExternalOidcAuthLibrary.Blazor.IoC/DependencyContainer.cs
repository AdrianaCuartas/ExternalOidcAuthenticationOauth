namespace ExternalOidcAuthLibrary.Blazor.IoC;

public static class DependendencyContainer
{
    public static IServiceCollection AddBlazorExternalOidcAuthLibraryServices(
        this IServiceCollection services, Action<AppOptions> appOptionsSetter)
    {

        services.AddExternalOidcAuthLibraryBlazorUIServices();
        services.Configure<AppOptions>(appOptionsSetter);

        return services;
    }
}