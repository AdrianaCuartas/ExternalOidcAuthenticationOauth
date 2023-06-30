namespace Membership.Blazor.WebApiGateway;
public class UserWebApiGateway : IUserWebApiGateway
{
    readonly UserEndpointsOptions Options;
    readonly HttpClient Client;

    public UserWebApiGateway(IOptions<UserEndpointsOptions> options,
        IHttpClientFactory factory)
    {
        Options = options.Value;
        Client = factory.CreateClient(nameof(IUserWebApiGateway));
        Client.BaseAddress = new Uri(Options.WebApiBaseAddress);
    }

    //public UserWebApiGateway(IOptions<UserEndpointsOptions> options,
    //    HttpClient client)
    //{
    //    Options = options.Value;
    //    Client = client;
    //    Client.BaseAddress = new Uri(Options.WebApiBaseAddress);
    //}

    public async Task<UserTokensDto> LoginAsync(LocalUserCredentialsDto userCredentials)
    {
        var Response = await Client.PostAsJsonAsync(
             Options.Login,
             userCredentials);
        return await Response.Content.ReadFromJsonAsync<UserTokensDto>();
    }

    public async Task LogoutAsync(string refreshToken)
    {
        await Client.PostAsJsonAsync(Options.Logout, refreshToken);
    }

    public async Task<UserTokensDto> RefreshTokenAsync(UserTokensDto userTokens)
    {
        var Response = await Client.PostAsJsonAsync(Options.RefreshToken, userTokens);
        return await Response.Content.ReadFromJsonAsync<UserTokensDto>();
    }

    public async Task RegisterUserAsync(UserForRegistrationDto userData)
    {
        await Client.PostAsJsonAsync(Options.Register, userData);

    }


}