namespace Common.Models
{
    public class AuthResponse
    {
        public bool Result { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
    }
}