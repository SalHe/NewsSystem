using System.Collections.Generic;
using System.Threading.Tasks;
using Whu.BLM.NewsSystem.Shared.Entity.Content;

namespace Whu.BLM.NewsSystem.Client.Services.Impl
{
    public class NewsCategoryService : INewsCategoryService
    {
        public Task<IList<NewsCategory>> GetNewsCategoriesAsync()
        {
            // TODO 获取新闻类别
            IList<NewsCategory> newsCategories = new List<NewsCategory>();
            var categories = new[] {"军事", "政治", "娱乐", "经济", "国际"};
            int i = 0;
            foreach (var categoryName in categories)
            {
                newsCategories.Add(new NewsCategory
                {
                    Id = i,
                    Name = categoryName,
                    News = new List<News>()
                });
                i++;
            }

            return Task.FromResult(newsCategories);
        }
    }
}