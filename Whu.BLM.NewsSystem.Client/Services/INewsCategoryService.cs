using System.Collections.Generic;
using System.Threading.Tasks;
using Whu.BLM.NewsSystem.Shared.Entity.Content;

namespace Whu.BLM.NewsSystem.Client.Services
{
    public interface INewsCategoryService
    {
        Task<IList<NewsCategory>> GetNewsCategoriesAsync();
    }
}