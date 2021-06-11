using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Whu.BLM.NewsSystem.Server.Data.Context;
using Whu.BLM.NewsSystem.Spider.Adapter;
using Whu.BLM.NewsSystem.Spider.Pages;

namespace Whu.BLM.NewsSystem.Spider
{
    public class SpiderScheduler
    {
        private class PageAndSpider
        {
            public BasePage Page { get; set; }
            public ISpider Spider { get; set; }
        }

        public SpiderRepository SpiderRepository { get; }
        private readonly NewsSystemContext _newsSystemContext;
        private readonly ConcurrentQueue<PageAndSpider> _pageAndSpiders;

        public SpiderScheduler(SpiderRepository spiderRepository, NewsSystemContext newsSystemContext)
        {
            SpiderRepository = spiderRepository;
            _newsSystemContext = newsSystemContext;
            _pageAndSpiders = new ConcurrentQueue<PageAndSpider>();
        }

        public void StartTask()
        {
            HandleTasks().Start();
        }

        private async Task HandleTasks()
        {
            while (true)
            {
                if (_pageAndSpiders.TryDequeue(out PageAndSpider pageAndSpider))
                {
                    var spider = pageAndSpider.Spider;
                    if (pageAndSpider.Page is HomePage homePage)
                    {
                        await HandleHomePage(spider, homePage);
                    }
                    else if (pageAndSpider.Page is CategoryPage categoryPage)
                    {
                        await HandleCategoryPage(spider, categoryPage);
                    }
                    else if (pageAndSpider.Page is NewsPage newsPage)
                    {
                        await HandleNewsPage(spider, newsPage);
                    }
                }
            }
        }

        protected async Task HandleNewsPage(ISpider spider, NewsPage newsPage)
        {
            // TODO 存到数据库
            Console.WriteLine($"【{newsPage.CategoryPage.CategoryName}】{newsPage.Title}, {newsPage.Url}");
        }

        protected async Task HandleCategoryPage(ISpider spider, CategoryPage categoryPage)
        {
            foreach (var newsPage in await spider.AnalyseCategoryPage(categoryPage))
            {
                _pageAndSpiders.Enqueue(new PageAndSpider()
                {
                    Page = newsPage,
                    Spider = spider
                });
            }
        }

        protected async Task HandleHomePage(ISpider spider, HomePage homePage)
        {
            foreach (var categoryPage in await spider.AnalyseHomePage(homePage))
            {
                _pageAndSpiders.Enqueue(new PageAndSpider()
                {
                    Page = categoryPage,
                    Spider = spider
                });
            }
        }

        public async Task AddTaskAsync(string homeUrl)
        {
            ISpider spider = await SpiderRepository.FindSupportSpiderForHomePage(homeUrl);
            HomePage homePage = await spider.AnalyseUrl(homeUrl);
            _pageAndSpiders.Enqueue(new PageAndSpider
            {
                Page = homePage,
                Spider = spider
            });
        }
    }
}