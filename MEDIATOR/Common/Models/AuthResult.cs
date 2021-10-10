namespace MEDIATOR.Common.Models
{
    public class AuthResult
    {
        public bool Result { get; set; } = false;
        public string Token { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}