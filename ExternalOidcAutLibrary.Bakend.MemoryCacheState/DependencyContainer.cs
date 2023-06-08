namespace ExternalOidcAutLibrary.Bakend.MemoryCacheState;

public static class DependendencyContainer
{
    public static IServiceCollection AddMemoryCacheStateServices(
        this IServiceCollection services)
    {
        services.TryAddSingleton<IMemoryCache, MemoryCache>();
        services.TryAddSingleton<IApiStateService, MemoryCacheStateService>();

        return services;
    }
}