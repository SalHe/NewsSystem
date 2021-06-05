using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Whu.BLM.NewsSystem.Server.Data.Context;
using Whu.BLM.NewsSystem.Shared;
using Whu.BLM.NewsSystem.Shared.Entity.Content;

namespace Whu.BLM.NewsSystem.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController : ControllerBase
    {
        public NewsSystemContext NewsSystemContext { get; set; }

        public NewsController(NewsSystemContext newsSystemContext)
        {
            NewsSystemContext = newsSystemContext;
        }
        /// <summary>
        /// 带有页码的新闻结构体（建立特定新闻与页码的映射关系）。
        /// </summary>
        public struct NewsWithPage
        {
            public News news;
            public int page;
        }
        public struct releaseModel
        {
            public int id;public string title;public string abstractcontent;public string originalURL;public NewsCategory category;
        }
        public struct changeModel
        {
            public int id;public string title; public string content;public NewsCategory category;
        }
        /// <summary>
        /// 检索一个新闻中是否存在相应字段。
        /// </summary>
        [NonAction]
        public List<News> Search(string searchWord, int numOfPage)
        {
            var page = NewsSystemContext.News.Where(news => news.Title.Contains(searchWord))
                .Skip(10 * numOfPage)
                .Take(10).ToList();
            return page;
            
        }

        /// <summary>
        /// 返回特定搜索字段的含页码的新闻列表。
        /// </summary>
        [HttpGet("newsBySearchWord/{searchWord}")]
        public List<NewsWithPage> ListOfNewsWithPages(string searchWord)
        {
            List<NewsWithPage> list = new List<NewsWithPage>();
            try
            {
                for (int i = 1; i <= 10; i++)
                {
                    List<News> singleList = Search(searchWord, i);
                    foreach (var news in singleList)
                    {
                        NewsWithPage newsWithPage = new NewsWithPage();
                        newsWithPage.news = news;
                        newsWithPage.page = i;
                        list.Add(newsWithPage);
                    }
                }
                return list;
            }
            catch
            {
                return list;
            }

        }

        /// <summary>
        /// 返回指定类别的含页码的新闻列表。
        /// </summary>
        [HttpGet("newsByCategory/{idOfCategory}")]
        public List<NewsWithPage> ListOfNewsWithPages(int idOfCategory)
        {
            List<NewsWithPage> list = new List<NewsWithPage>();
            try
            {
                for (int i = 1; i <= 10; i++)
                {
                    List<News> singleList = NewsSystemContext.News.Where(news => news.NewsCategory.Id == idOfCategory)
                .Skip(10 * i)
                .Take(10).ToList();
                    foreach (var news in singleList)
                    {
                        NewsWithPage newsWithPage = new NewsWithPage();
                        newsWithPage.news = news;
                        newsWithPage.page = i;
                        list.Add(newsWithPage);
                    }
                }
                return list;
            }
            catch
            {
                return list;
            }

        }
        /// <summary>
        /// 删除指定新闻ID的新闻。
        /// </summary>
        [HttpDelete("News/{IdOfNews}")]
        public bool DeleteNews(int IdOfNews)
        {
            try
            {
                var item = NewsSystemContext.News.Where(news => news.Id == IdOfNews);
                NewsSystemContext.Remove(item);
                NewsSystemContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 发布新闻。
        /// </summary>
        [HttpPost("News/{mdl}")]
        public bool ReleaseNews(releaseModel mdl)
        {
            try
            {
                News news = new News();
                news.Id = mdl.id;
                news.Title = mdl.title;
                news.AbstractContent = mdl.abstractcontent;
                news.OringinUrl = mdl.originalURL;
                news.NewsCategory = mdl.category;
                var categoryInDBSet = NewsSystemContext.NewsCategories.Where(categoryInDBSet => categoryInDBSet.Name == mdl.category.Name).FirstOrDefault();
                categoryInDBSet.News.Add(news);
                NewsSystemContext.News.Add(news);
                NewsSystemContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 调整新闻。
        /// </summary>
        [HttpPut("News/{mdl}")]
        public bool ChangeNews(changeModel mdl)
        {
            try
            {
                var news = NewsSystemContext.News.Where(news => news.Id == mdl.id).FirstOrDefault();
                news.AbstractContent = mdl.content;
                news.Title = mdl.title;
                if(news.NewsCategory != mdl.category)
                {
                    news.NewsCategory = mdl.category;
                    var categoryInDBSet = NewsSystemContext.NewsCategories.Where(categoryInDBSet => categoryInDBSet.Name == mdl.category.Name).FirstOrDefault();
                    categoryInDBSet.News.Add(news);
                }
                NewsSystemContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}