namespace ExternalOidcAuthLibrary.Backend.MemoryClients.Options;

public class ClientConfigurationOptions
{
    public const string SectionKey = "Clients";
    public Dictionary<string, ClientConfiguration> Clients { get; set; } = new();

    public ClientConfiguration this[string clientId]
    {
        get
        {
            ClientConfiguration Result = null;

            Clients.TryGetValue(clientId, out Result);
            return Result;
        }
    }
}
