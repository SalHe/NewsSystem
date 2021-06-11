using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Whu.BLM.NewsSystem.Client.Exceptions;
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

        public IList<NewsCategory> CachedNewsCategories { get; set; }

        private News GenerateNews(int id, int? categoryId = null)
        {
            Random random = new Random();
            var category = categoryId == null
                ? CachedNewsCategories[random.Next(0, CachedNewsCategories.Count)]
                : CachedNewsCategories.FirstOrDefault(x =>
                    x.Id == categoryId);
            return new News
            {
                Id = id,
                Title = $"News {id}",
                NewsCategory = category,
                OringinUrl = $"https://www.baidu.com/s?wd=news{id}",
                AbstractContent = $"Content{id}"
            };
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
            // TODO 无分类获取新闻列表
            IList<News> news = new List<News>();
            for (int i = 0; i < 10; i++)
            {
                news.Add(GenerateNews(i));
            }

            return news;
        }

        public async Task<IList<News>> GetNewsListAsync(int newsCategoryId, int page, int size)
        {
            // TODO 按分类获取新闻列表
            IList<News> news = new List<News>();
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                news.Add(GenerateNews(i, newsCategoryId));
            }

            return news;
        }

        public Task<IList<News>> SearchNewsAsync(string keyword, int page, int size)
        {
            return GetNewsListAsync(page, size);
        }
    }
}