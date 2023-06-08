namespace ExternalOidcAuthLibrary.Blazor.UI.Services;
internal class AppStateService : IAppStateService
{
    const string SessionKey = "externalOidcAuthLibrary";
    readonly IJSRuntime JSRuntime;
    readonly Lazy<Task<Dictionary<string, string>>> GetCurrentStateTask;

    public AppStateService(IJSRuntime jsRuntime)
    {
        JSRuntime = jsRuntime;
        GetCurrentStateTask = new Lazy<Task<Dictionary<string, string>>>(() =>
                    GetCurrentStateAsync());
    }
    public async Task<T> GetItemAsync<T>(string key)
    {
        T Value = default;
        var State = await GetCurrentStateTask.Value;
        try
        {
            if (State.ContainsKey(key))
            {
                //se deserializa un item del diccionario
                Value = JsonSerializer.Deserialize<T>(State[key]);
            }
        }
        catch { }
        return Value;
    }
    public async Task RemoveItemAsync(string key)
    {
        var State = await GetCurrentStateTask.Value;
        if (State.Remove(key))
        {
            await SaveCurrentStateAsync();
        }
    }
    public async Task SetItemAsync<T>(string key, T value)
    {
        var State = await GetCurrentStateTask.Value;
        var SerializedValue = JsonSerializer.Serialize(value);
        if (State.ContainsKey(key))
        {
            State[key] = SerializedValue;
        }
        else
        {
            State.Add(key, SerializedValue);
        }
        await SaveCurrentStateAsync();
    }

    #region Save, Get State
    async Task<Dictionary<string, string>> GetCurrentStateAsync()
    {
        //se guarda en sesion 
        string StateString =
            await JSRuntime.InvokeAsync<string>("sessionStorage.getItem", SessionKey);

        Dictionary<string, string> Values;
        if (!string.IsNullOrWhiteSpace(StateString))
        {
            //javascript quiere que todo lo guardado este serializado
            StateString = StateString.FromBase64Url();

            //se desealiza porque cuando se guarda se lleva en base64URL
            Values = JsonSerializer.Deserialize<Dictionary<string, string>>(StateString);


        }
        else
        {
            Values = new();
        }
        return Values;
    }

    async Task SaveCurrentStateAsync()
    {
        var State = await GetCurrentStateTask.Value;
        //al aplicarle el Base64url al objeto json dado por JsonSerializer.Serialize(State)
        //se  le da un poco de ofuscacion al diccionario.
        var StateString = JsonSerializer.Serialize(State).ToBase64Url();
        //tambien para protegerlo se podria aplicar un algorito de hast que sea reversible

        await JSRuntime.InvokeVoidAsync("sessionStorage.setItem", SessionKey, StateString);
    }

    #endregion
}
