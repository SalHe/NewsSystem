using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Whu.BLM.NewsSystem.Client.Authentication;
using Whu.BLM.NewsSystem.Client.Services;
using Whu.BLM.NewsSystem.Client.Services.Impl;

namespace Whu.BLM.NewsSystem.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            // 登录认证
            builder.Services.AddOidcAuthentication(options =>
                builder.Configuration.Bind("AuthenticationConfig", options));
            
            builder.Services.AddScoped(sp =>
            {
                string baseUr = builder.HostEnvironment.IsDevelopment()
                    ? builder.Configuration.GetSection("NewsServer")["Url"]
                    : builder.HostEnvironment.BaseAddress;
                return new HttpClient {BaseAddress = new Uri(baseUr)};
            });
            builder.Services.AddScoped<INewsCategoryService, NewsCategoryService>();
            builder.Services.AddScoped<INewsService, NewsService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

            builder.Services.AddAuthorizationCore();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<AuthenticationStateProvider, MyAuthenticationStateProvider>();

            // Add support for Ant Design
            builder.Services.AddAntDesign();

            await builder.Build().RunAsync();
        }
    }
}