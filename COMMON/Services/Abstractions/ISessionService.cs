namespace Common.Services.Abstractions
{
    public interface ISessionService
    {
        void SetToken(string token);
        void SetMail(string email);
        void SetIdentifier(string id);
        string GetToken();
        string GetMail();
        string GetId();
    }
}