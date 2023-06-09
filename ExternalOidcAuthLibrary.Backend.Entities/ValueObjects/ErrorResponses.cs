namespace ExternalOidcAuthLibrary.Backend.Entities.ValueObjects;

public static class ErrorResponses
{

    public static ErrorResponse InvalidState => new ErrorResponse("Invalid state", "Invalid state");

    public static ErrorResponse UnsuppoertedGrantType => new ErrorResponse("Unsupported_grant_type", "Unsupported_grant_type");

    public static ErrorResponse InvalidRedirectUri => new ErrorResponse("invalid_request", "Unauthorized redirect_uri");


    public static ErrorResponse InvalidClient => new ErrorResponse("invalid_client", "Client authentication failed");


    public static ErrorResponse InvalidScope => new ErrorResponse("invalid_scope", "Unsupported_grant_type");


    public static ErrorResponse InvalidCodeVerifier => new ErrorResponse("invalid_grant", "Invalid code_verifier");


    public static ErrorResponse InvalidRequest => new ErrorResponse("invalid_request", "Invalid request");


    public static ErrorResponse UnableToGetExternalTokens => new ErrorResponse("internal error", "Unable to get external tokens");

}
