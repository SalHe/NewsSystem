using HtmlAgilityPack;
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

        HomePage _homePage = new HomePage();

        public NeteaseSpider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<bool> CanSupport(string url)
        {
            return Task.FromResult(url.Contains("163.com"));
        }

        public async Task<HomePage> AnalyseUrl(string url)
        {

            var html = _httpClient.GetStringAsync(url).Result;

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            string str = htmlDocument.DocumentNode.InnerHtml;
            _homePage.Html = htmlDocument.DocumentNode.InnerHtml;
            _homePage.Url = url;

            if (url != null)
                return _homePage;
            else
                return null;
        }

        /// <summary>
        /// 分析首页的分类情况
        /// </summary>
        /// <param name="homePage"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CategoryPage>> AnalyseHomePage(HomePage homePage)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(homePage.Html);
            HtmlNodeCollection nodes_1 = htmlDocument.DocumentNode.SelectNodes("//div[@class='N-nav-channel JS_NTES_LOG_FE']/a");

            List<CategoryPage> _categoryPage = new List<CategoryPage>();

            foreach (var node in nodes_1)
            {
                string href = node.Attributes["href"].Value;
                string text = node.InnerText;
                Console.WriteLine("属性值：" + href + "\t" + "标签内容:" + text);
                CategoryPage smallcategoryPage = new CategoryPage();
                smallcategoryPage.CategoryName = text;
                smallcategoryPage.HomePage = homePage;
                smallcategoryPage.Url = href;
                smallcategoryPage.Html = htmlDocument.DocumentNode.InnerHtml;
                _categoryPage.Add(smallcategoryPage);
            }

            if (nodes_1 != null)
                return _categoryPage;
            else return null;
        }

        public async Task<IEnumerable<NewsPage>> AnalyseCategoryPage(CategoryPage categoryPage)
        {
            List<NewsPage> _newsPage = new List<NewsPage>();
            
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(categoryPage.Html);

            HtmlNodeCollection nodes = htmlDocument.DocumentNode.SelectNodes("//div[contains(@class,'hidden') and contains(@ne-if,'i')]/div");


            foreach (var node in nodes)
            {
                HtmlNode node_1 = node.SelectSingleNode(node.XPath + "/a");

                NewsPage smallnewsPage = new NewsPage();

                smallnewsPage.Url = node_1.Attributes["href"].Value;
                smallnewsPage.Title = node_1.InnerText;
                smallnewsPage.CategoryPage = categoryPage;
               // Console.WriteLine("目标网址：" + smallnewsPage.Url + "\t" + "标题:" + smallnewsPage.Title  + "\t");

                _newsPage.Add(smallnewsPage);
            }

            if (nodes != null)
                return _newsPage;
            else return null;
        }
    }
}