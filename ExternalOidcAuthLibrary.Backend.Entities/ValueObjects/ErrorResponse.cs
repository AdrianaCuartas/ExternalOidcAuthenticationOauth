namespace ExternalOidcAuthLibrary.Backend.Entities.ValueObjects;

public class ErrorResponse
{
    public ErrorResponse(string error, string error_Description)
    {
        Error = error;
        Error_Description = error_Description;
    }

    public string Error { get; set; }

    public string Error_Description { get; set; }

}
