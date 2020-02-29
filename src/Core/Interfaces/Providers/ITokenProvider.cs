using System.Threading.Tasks;

namespace Core.Interfaces.Providers
{
    public interface ITokenProvider
    {
        Task<string> Authenticate(string userName, string password);
    }
}