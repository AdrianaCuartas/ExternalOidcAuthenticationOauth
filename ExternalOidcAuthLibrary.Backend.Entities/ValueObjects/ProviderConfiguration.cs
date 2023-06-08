namespace ExternalOidcAuthLibrary.Backend.Entities.ValueObjects
{
    public class ProviderConfiguration
    {
        public string Authority { get; set; }
        public string ClientId { get; set; }
        public string Authorization_Endpoint { get; set; }

        public string Token_Endpoint { get; set; }

        public string ClientSecret { get; set; }

        public string RedirectUri { get; set; }

        public bool SupportS256ChallengeMethod { get; set; }
    }
}
