using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
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

        public SpiderRepository SpiderRepository { get; private set; }
        private ConcurrentQueue<PageAndSpider> _pageAndSpiders;

        public SpiderScheduler() : this(new SpiderRepository())
        {
        }

        public SpiderScheduler(SpiderRepository spiderRepository)
        {
            SpiderRepository = spiderRepository;
            _pageAndSpiders = new ConcurrentQueue<PageAndSpider>();
        }

        public void AddTask(string homeUrl)
        {
            // TODO 分析URL并加入任务池
        }
    }
}