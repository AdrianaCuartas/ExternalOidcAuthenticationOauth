using ExternalOidcAuthLibrary.Backend.Entities.Enums;

namespace ExternalOidcAuthLibrary.Backend.Entities.ValueObjects;

public class RequestState
{
    public string CodeVerifier { get; set; }
    public string Nonce { get; set; }
    public Provider Provider { get; set; }

    public RequestStateClientData ClientData { get; set; }
}
