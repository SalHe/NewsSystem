using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whu.BLM.NewsSystem.Shared.Entity.Content;

namespace Whu.BLM.NewsSystem.Client.Services.Impl
{
    public class NewsService : INewsService
    {
        private INewsCategoryService NewsCategoryService { get; }

        public NewsService(INewsCategoryService newsCategoryService)
        {
            NewsCategoryService = newsCategoryService;
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

        public Task<News> GetNewsById(int id)
        {
            // TODO 通过新闻ID获取新闻
            return Task.FromResult(GenerateNews(id));
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