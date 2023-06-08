namespace ExternalOidcAuthLibrary.Tools;

public static class PKCE
{
    public static string GetCodeVerifier()
    {
        const string PossibleChars = "ABCEDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-._~";

        StringBuilder SB = new StringBuilder();
        int MaxIndex = PossibleChars.Length;
        Random RandomGenerator = new Random();

        int Length = RandomGenerator.Next(43, 129);
        for (int i = 0; i < Length; i++)
        {
            SB.Append(PossibleChars[RandomGenerator.Next(MaxIndex)]);
        }
        return SB.ToString();
    }

    public static string GetHash256CodeChallenge(string codeVerifier)
    {
        using var Sha256 = SHA256.Create();
        var ChallengeBytes =
            Sha256.ComputeHash(Encoding.UTF8.GetBytes(codeVerifier));

        return Base64UrlEncoder.Encode(ChallengeBytes);
    }
}
