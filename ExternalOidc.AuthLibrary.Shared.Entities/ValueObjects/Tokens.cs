using System.Text.Json.Serialization;

namespace ExternalOidcAuthLibrary.Shared.Entities.ValueObjects;

public class Tokens
{
    [JsonPropertyName("id_token")]
    public string Id_Token { get; set; }
    [JsonPropertyName("access_token")]
    public string Access_Token { get; set; }
    [JsonPropertyName("refresh_token")]
    public string Refresh_Token { get; set; }
}
