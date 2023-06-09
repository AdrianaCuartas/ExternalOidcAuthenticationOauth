namespace ExternalOidcAuthLibrary.Backend.Entities.Interfaces;

public interface IApiStateService
{
    Task SetItemAsync(string state, RequestState stateValue);

    Task<RequestState> GetItemStateAsync(string state);


    Task RemoveItemSateAsync(string state);

}
