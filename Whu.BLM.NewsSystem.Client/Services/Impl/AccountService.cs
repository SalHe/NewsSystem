using System;
using System.Net.Http;
using System.Threading.Tasks;
using Whu.BLM.NewsSystem.Client.Exceptions;

namespace Whu.BLM.NewsSystem.Client.Services.Impl
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;

        public AccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> Login(string username, string password)
        {
            var response = await _httpClient.GetAsync($"api/account/login?username={username}&password={password}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            throw new LoginErrorException(await response.Content.ReadAsStringAsync());
        }
    }
}