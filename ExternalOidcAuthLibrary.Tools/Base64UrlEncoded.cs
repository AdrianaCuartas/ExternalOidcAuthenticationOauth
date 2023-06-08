namespace ExternalOidcAuthLibrary.Tools;
public static class Base64UrlEncoded
{
    //se crean cadenas aleatorias que puede sen enviadas en una url
    //se recomienda que la logintud  del parametro sea un multiplo de 4
    //para que el resultado sea mas exacto
    public static string GetRandomString(int length)
    {
        //3 bytes generan 4 caracteres
        /*
          3 bytes -  4 caracteres
           X      -  length
          Se aplica una regla de 3.
          */

        double NumBytes = Math.Ceiling(3 * length / 4d);

        byte[] Buffer = new byte[(int)NumBytes];
        using var Rng = RandomNumberGenerator.Create();
        Rng.GetNonZeroBytes(Buffer);
        return Base64UrlEncoder.Encode(Buffer);
    }

}