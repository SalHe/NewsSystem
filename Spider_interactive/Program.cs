using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using HtmlAgilityPack;

namespace Spider_interactive
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);


            var httpClient = new HttpClient();
            var html = httpClient.GetStringAsync("https://news.163.com/").Result;

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            // 网址集合
            string[] href_collection = new string[100];

            int i = 0;

            string str = htmlDocument.DocumentNode.InnerHtml;


            HtmlNodeCollection nodes_1 = htmlDocument.DocumentNode.SelectNodes("//div[@class='N-nav-channel JS_NTES_LOG_FE']/a");

            foreach (var node in nodes_1)
            {
                string href = node.Attributes["href"].Value;
                string text = node.InnerText;
                Console.WriteLine("属性值：" + href + "\t" + "标签内容:" + text);
                href_collection[i] = href;
                i++;
            }

            Pachong(href_collection[0]);

            //HtmlNodeCollection nodes_2 = htmlDocument.DocumentNode.SelectNodes("//div[@class='ns_area list']/ul/li/a");

            //foreach (var node in nodes_2)
            //{
            //    string href = node.Attributes["href"].Value;
            //    string text = node.InnerText;
            //    Console.WriteLine("属性值：" + href + "\t" + "标签内容:" + text);
            //}
            Console.ReadKey();    
        }


        static void Pachong(string url)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);


            var httpClient = new HttpClient();
            var html = httpClient.GetStringAsync(url).Result;
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            HtmlNodeCollection nodes = htmlDocument.DocumentNode.SelectNodes("//div[contains(@class,'hidden') and contains(@ne-if,'i')]/div");
            

            foreach (var node in nodes)
            {
                HtmlNode node_1 = node.SelectSingleNode(node.XPath + "/a");
                string href = node_1.Attributes["href"].Value;
                string text = node_1.InnerText;
                Console.WriteLine("属性值：" + href + "\t" + "标签内容:" + text);
            }
        }

    }
}
