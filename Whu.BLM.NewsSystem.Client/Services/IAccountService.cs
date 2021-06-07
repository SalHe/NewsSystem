using System.Threading.Tasks;

namespace Whu.BLM.NewsSystem.Client.Services
{
    public interface IAccountService
    {
        Task<string> Login(string username, string password);
    }
}