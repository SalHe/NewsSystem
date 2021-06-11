using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Whu.BLM.NewsSystem.Spider.Adapter;
using Whu.BLM.NewsSystem.Spider.Exceptions;
using Whu.BLM.NewsSystem.Spider.Pages;

namespace Whu.BLM.NewsSystem.Spider
{
    public class SpiderRepository
    {
        private IList<ISpider> _spiders;

        public SpiderRepository()
        {
            _spiders = new List<ISpider>();
        }

        public void RegisterSpider(ISpider spider)
        {
            _spiders.Add(spider);
        }

        public void UnregisterSpider(ISpider spider)
        {
            _spiders.Remove(spider);
        }

        public async Task<ISpider> FindSupportSpiderForHomePage(string url)
        {
            foreach (var spider in _spiders)
            {
                if (await spider.CanSupport(url))
                {
                    return spider;
                }
            }

            return null;
        }

        public async Task<IEnumerable<CategoryPage>> AnalyseHomePage(HomePage homePage)
        {
            foreach (var spider in _spiders)
            {
                try
                {
                    var categoryPages = await spider.AnalyseHomePage(homePage);
                    if (categoryPages != null)
                        return categoryPages;
                }
                catch (BaseSpiderException e)
                {
                    Console.WriteLine(e);
                    // throw;
                }
            }

            throw new UnsupportedNewsWebException("未找到可以分析此URL的爬虫");
        }

        public async Task<IEnumerable<NewsPage>> AnalyseCategoryPage(CategoryPage categoryPage)
        {
            foreach (var spider in _spiders)
            {
                try
                {
                    var newsPages = await spider.AnalyseCategoryPage(categoryPage);
                    if (newsPages != null)
                        return newsPages;
                }
                catch (BaseSpiderException e)
                {
                    Console.WriteLine(e);
                    // throw;
                }
            }

            throw new UnsupportedNewsWebException("未找到可以分析此URL的爬虫");
        }
    }
}