namespace ExternalOidcAuthLibrary.Shared.Entities.Dtos;

public class ExternalUserDto
{

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Sub { get; set; }

    public string Provider { get; set; }

    public string Nonce { get; set; }
}
