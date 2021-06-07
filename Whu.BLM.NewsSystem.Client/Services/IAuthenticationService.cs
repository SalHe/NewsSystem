using System.Threading.Tasks;

namespace Whu.BLM.NewsSystem.Client.Services
{
    public interface IAuthenticationService
    {
        Task Login(string username, string password);
    }
}