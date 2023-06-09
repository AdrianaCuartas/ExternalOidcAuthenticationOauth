using ExternalOidcAuthLibrary.Shared.Entities.ValueObjects;

namespace ExternalOidcAuthLibrary.Backend.Entities.ValueObjects;

public class RequestStateClientData
{
    public string ClientId { get; set; }
    public string RedirectUri { get; set; }

    public string Scope { get; set; }

    public string State { get; set; }

    public string CodeChallenge { get; set; }

    public string CodeChallengeMethod { get; set; }

    public string Nonce { get; set; }

    public Tokens Tokens { get; set; }
}
