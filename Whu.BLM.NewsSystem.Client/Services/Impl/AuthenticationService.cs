﻿using System;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Whu.BLM.NewsSystem.Client.Authentication;

namespace Whu.BLM.NewsSystem.Client.Services.Impl
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAccountService _accountService;
        private readonly ILocalStorageService _localStorageService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthenticationService(IAccountService accountService, ILocalStorageService localStorageService,
            AuthenticationStateProvider authenticationStateProvider)
        {
            _accountService = accountService;
            _localStorageService = localStorageService;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task Login(string username, string password)
        {
            var token = await _accountService.Login(username, password);
            await _localStorageService.SetItemAsStringAsync("token", token);
            (_authenticationStateProvider as MyAuthenticationStateProvider)?.NotifyLogin(token);
        }

        public async Task Logout()
        {
            await _localStorageService.RemoveItemAsync("token");
            (_authenticationStateProvider as MyAuthenticationStateProvider)?.NotifyLogout();
        }
    }
}