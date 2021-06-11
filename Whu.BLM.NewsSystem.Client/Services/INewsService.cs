using System.Collections.Generic;
using System.Threading.Tasks;
using Whu.BLM.NewsSystem.Shared.Entity.Content;

namespace Whu.BLM.NewsSystem.Client.Services
{
    public interface INewsService
    {
        public const int PageSize = 10;

        Task<News> GetNewsById(int id);
        Task<IList<News>> GetNewsListAsync(int page, int size);
        Task<IList<News>> GetNewsListAsync(int newsCategoryId, int page, int size);
        Task<IList<News>> SearchNewsAsync(string keyword, int page, int size);
        Task<News> AddNews(int categoryId, News news);
        Task<News> UpdateNews(int categoryId, News news);
        Task DeleteNews(int newsId);

    }
}