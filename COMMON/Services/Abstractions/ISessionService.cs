namespace Common.Services.Abstractions
{
    public interface ISessionService
    {
        void SetToken(string token);
        void SetMail(string email);
        string GetToken();
    }
}