namespace ExternalOidcAutLibrary.Bakend.MemoryCacheState;

public class MemoryCacheStateService : IApiStateService
{
    readonly IMemoryCache Cache;
    public MemoryCacheStateService(IMemoryCache cache)
    {
        Cache = cache;
    }
    public Task<RequestState> GetItemSateAsync(string state)
    {
        Cache.TryGetValue(state, out RequestState statevalue);
        return Task.FromResult(statevalue);
    }

    public Task RemoveItemSateAsync(string state)
    {
        Cache.Remove(state);
        return Task.CompletedTask;
    }

    public Task SetItemAsync(string state, RequestState stateValue)
    {
        Cache.Set(state, stateValue, DateTime.Now.AddMinutes(10));

        return Task.CompletedTask;

    }
}