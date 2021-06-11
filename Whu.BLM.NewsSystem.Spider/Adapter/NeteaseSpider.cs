using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Whu.BLM.NewsSystem.Spider.Pages;

namespace Whu.BLM.NewsSystem.Spider.Adapter
{
    public class NeteaseSpider : ISpider
    {
        private readonly HttpClient _httpClient;

        public NeteaseSpider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<bool> CanSupport(string url)
        {
            return Task.FromResult(url.Contains("163.com"));
        }

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