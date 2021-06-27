using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using HtmlAgilityPack;
using Whu.BLM.NewsSystem.Shared.Entity.Content;
using Whu.BLM.NewsSystem.Server.Data.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Whu.BLM.NewsSystem.Spider.Adapter;
using Microsoft.Extensions.Configuration;
using Whu.BLM.NewsSystem.Spider;

namespace Spider_interactive
{
    class Spider_interactive
    {
        public static NewsSystemContext NewsSystemContext { get; private set; }

        static void Main(string[] args)
        {

            Console.WriteLine("start");

            var config = new ConfigurationBuilder().AddUserSecrets(typeof(Spider_interactive).Assembly).Build();
            var connectionString = config.GetConnectionString("MySqlConnection");
            var options = new DbContextOptionsBuilder<NewsSystemContext>().UseMySql(connectionString,
                new MySqlServerVersion("8.0.0")).Options;
            NewsSystemContext = new NewsSystemContext(options);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var httpClient = new HttpClient();

            NeteaseSpider netease = new NeteaseSpider(httpClient);


            SpiderRepository _spiderrepository = new SpiderRepository();

            _spiderrepository.RegisterSpider(netease);

            SpiderScheduler _spiderscheduler = new SpiderScheduler(_spiderrepository, NewsSystemContext);

            _spiderscheduler.AddTaskAsync("https://news.163.com/");

            _spiderscheduler.StartTask();

            Console.WriteLine("ww");

        }

    }
}