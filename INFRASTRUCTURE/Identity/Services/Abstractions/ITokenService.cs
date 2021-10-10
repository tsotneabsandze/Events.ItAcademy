using System.Threading.Tasks;

namespace INFRASTRUCTURE.Identity.Services.Abstractions
{
    public interface ITokenService
    {
        Task<string> CreateTokeAsync(string mail);
    }
}