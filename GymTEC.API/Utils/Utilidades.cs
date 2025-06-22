namespace GymTEC.API.Utils
{
    public static class Utilidades
    {
        public static string EncriptarMD5(string texto)
        {
            using var md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(texto);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToUpperInvariant();
        }
    }
}
