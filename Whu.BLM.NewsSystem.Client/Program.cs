using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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

            builder.Services.AddScoped(sp =>
            {
                string baseUr = builder.Configuration.GetSection("NewsServer")["Url"];
                return new HttpClient {BaseAddress = new Uri(baseUr)};
            });
            builder.Services.AddScoped<INewsCategoryService, NewsCategoryService>();
            builder.Services.AddScoped<INewsService, NewsService>();

            // Add support for Ant Design
            builder.Services.AddAntDesign();

            await builder.Build().RunAsync();
        }
    }
}