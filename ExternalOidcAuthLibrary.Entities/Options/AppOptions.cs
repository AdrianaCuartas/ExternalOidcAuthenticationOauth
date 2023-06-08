using ExternalOidcAuthLibrary.Blazor.Entities.ValueObjects;

namespace ExternalOidcAuthLibrary.Blazor.Entities.Options;

public class AppOptions
{

    public const string SectionKey = nameof(AppOptions);

    public string ClientId { get; set; }

    public string Redirect_Uri { get; set; }

    public string Authorization_Endpoint { get; set; }

    public string Token_Endpoint { get; set; }


    public ProviderInfo[] Providers { get; set; }
}
