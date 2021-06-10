using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Whu.BLM.NewsSystem.Shared.Entity.Content;

namespace Whu.BLM.NewsSystem.Client.Services.Impl
{
    public class NewsCategoryService : INewsCategoryService
    {
        private readonly HttpClient _httpClient;

        public NewsCategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IList<NewsCategory>> GetNewsCategoriesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<NewsCategory>>("/api/category");
        }
    }
}