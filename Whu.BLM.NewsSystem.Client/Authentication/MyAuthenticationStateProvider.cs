using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace Whu.BLM.NewsSystem.Client.Authentication
{
    public class MyAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _httpClient;

        public MyAuthenticationStateProvider(ILocalStorageService localStorageService, HttpClient httpClient)
        {
            _localStorageService = localStorageService;
            _httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string token = await _localStorageService.GetItemAsStringAsync("token");
            if (string.IsNullOrWhiteSpace(token))
            {
                return new AuthenticationState(new ClaimsPrincipal());
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return AuthenticationStateFromJwt(token);
        }

        private static AuthenticationState AuthenticationStateFromJwt(string token)
        {
            // TODO 将jwt中包含的其他信息取出来
            return new AuthenticationState(
                new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {new Claim("user", "?")}, "jwt")));
        }

        public void NotifyLogout()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUser)));
        }

        public void NotifyLogin(string token)
        {
            NotifyAuthenticationStateChanged(Task.FromResult(AuthenticationStateFromJwt(token)));
        }
    }
}