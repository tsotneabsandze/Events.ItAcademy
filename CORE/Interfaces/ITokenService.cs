using System.Threading.Tasks;

namespace CORE.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateTokeAsync(string mail);
    }
}