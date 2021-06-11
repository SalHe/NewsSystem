using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Whu.BLM.NewsSystem.Client.Exceptions;
using Whu.BLM.NewsSystem.Server.Domain.VO;
using Whu.BLM.NewsSystem.Shared.Entity.Content;

namespace Whu.BLM.NewsSystem.Client.Services.Impl
{
    public class NewsService : INewsService
    {
        private readonly HttpClient _httpClient;

        public NewsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<News> GetNewsById(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<News>($"api/news/{id}");
            }
            catch (HttpRequestException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new NewsNotFoundException("找不到对应的新闻");
                }

                throw;
            }
        }

        public async Task<IList<News>> GetNewsListAsync(int page, int size)
        {
            return await _httpClient.GetFromJsonAsync<IList<News>>($"api/news/{page}/{size}");
        }

        public async Task<IList<News>> GetNewsListAsync(int newsCategoryId, int page, int size)
        {
            return await _httpClient.GetFromJsonAsync<IList<News>>($"api/news/{newsCategoryId}/{page}/{size}");
        }

        public Task<IList<News>> SearchNewsAsync(string keyword, int page, int size)
        {
            return GetNewsListAsync(page, size);
        }

        public async Task<News> AddNews(int categoryId, News news)
        {
            var model = new NewsApiModel.ReleaseModel
            {
                Category = categoryId,
                Title = news.Title,
                AbstractContent = news.AbstractContent,
                OriginalUrl = news.OringinUrl
            };
            var httpResponseMessage = await _httpClient.PostAsJsonAsync("api/news", model);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new AddNewsException(await httpResponseMessage.Content.ReadAsStringAsync());
            }

            return await httpResponseMessage.Content.ReadFromJsonAsync<News>();
        }

        public async Task<News> UpdateNews(int categoryId, News news)
        {
            var model = new NewsApiModel.ChangeModel
            {
                Id = news.Id,
                Title = news.Title,
                Content = news.AbstractContent,
                Category = categoryId
            };
            var httpResponseMessage = await _httpClient.PutAsJsonAsync("api/news", model);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new EditNewsException(await httpResponseMessage.Content.ReadAsStringAsync());
            }

            return await httpResponseMessage.Content.ReadFromJsonAsync<News>();
        }
    }
}