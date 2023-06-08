namespace ExternalOidcAuthLibrary.Backend.MemoryProviders.Options;

public class ProviderConfigurationOptions
{
    public const string SectionKey = "providers";

    public Dictionary<Provider, ProviderConfiguration> Providers { get; set; } = new();


    public ProviderConfiguration this[Provider provider]
    {
        get
        {
            ProviderConfiguration Result = null;

            Providers.TryGetValue(provider, out Result);
            return Result;
        }
    }
}
