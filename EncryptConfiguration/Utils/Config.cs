namespace EncryptConfiguration.Utils
{
    public class ConnectionStrings
    {
        public string DBConStr { get; set; }
    }
    public class Authentication
    {
        public string UserHashKey { get; set; }
    }

    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string SignKey { get; set; }
    }
}
