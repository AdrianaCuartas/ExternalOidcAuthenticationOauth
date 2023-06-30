namespace Membership.Blazor.WebApiGateway;

public class UserEndpointsOptions
{

    public const string SectionKey = "UserEndpoints";

    public string WebApiBaseAddress { get; set; }

    public string Register { get; set; }

    public string Login { get; set; }

    public string RefreshToken { get; set; }

    public string Logout { get; set; }
}
