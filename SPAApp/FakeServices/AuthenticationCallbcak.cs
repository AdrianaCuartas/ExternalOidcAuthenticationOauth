namespace SPAApp.FakeServices;

public class AuthenticationCallback : IAuthenticationCallback
{
    public Task Authentication(Tokens tokens)
    {
        Console.WriteLine("Autenticado");
        return Task.CompletedTask;

    }
}
