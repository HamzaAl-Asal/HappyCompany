namespace HappyCompany.JwtAuthentication.Options
{
    public class JWTAuthenticationOptions
    {
        public string SecretKey { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool ValidateAudience { get; set; }
    }
}