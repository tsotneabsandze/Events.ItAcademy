namespace INFRASTRUCTURE.Identity.Options
{
    public class JwtConfig
    {
        public string Secret { get; set; }
        public int Expiration { get; set; }
        
    }
}