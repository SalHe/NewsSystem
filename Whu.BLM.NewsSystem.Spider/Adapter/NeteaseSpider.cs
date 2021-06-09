using System.Collections.Generic;
using System.Threading.Tasks;
using Whu.BLM.NewsSystem.Spider.Pages;

namespace Whu.BLM.NewsSystem.Spider.Adapter
{
    public class NeteaseSpider : ISpider
    {
        public Task<HomePage> AnalyseUrl(string url)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<CategoryPage>> AnalyseHomePage(HomePage homePage)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<NewsPage>> AnalyseCategoryPage(CategoryPage categoryPage)
        {
            throw new System.NotImplementedException();
        }
    }
}