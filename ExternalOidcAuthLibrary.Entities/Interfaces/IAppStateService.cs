namespace ExternalOidcAuthLibrary.Blazor.Entities.Interfaces;

public interface IAppStateService
{

    Task SetItemAsync<T>(string key, T value);

    Task<T> GetItemAsync<T>(string key);

    Task RemoveItemAsync(string key);
}
