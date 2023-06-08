namespace ExternalOidcAuthLibrary.Tools.Extensions;

public static class StringExtensions
{
    public static string FromBase64Url(this string value) =>
        Base64UrlEncoder.Decode(value);

    public static string ToBase64Url(this string value) =>
       Base64UrlEncoder.Encode(value);
}
