using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Whu.BLM.NewsSystem.Client.Exceptions;
using Whu.BLM.NewsSystem.Server.Domain.VO;

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

        public async Task Register(string username, string password, string email)
        {
            // TODO 待实现email注册
            var model = new UserApiModel.RegisterModel()
            {
                Name =  username,
                Password = password
            };
            var response = await _httpClient.PostAsync("api/user/info", JsonContent.Create(model));
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            
            throw new RegisterException(await response.Content.ReadAsStringAsync());
        }
    }
}