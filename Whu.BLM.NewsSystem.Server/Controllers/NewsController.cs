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
        public class releaseModel
        {
            public int id { get; set; }
            public string title { get; set; }
            public string abstractcontent { get; set; }
            public string originalURL { get; set; }
            public NewsCategory category { get; set; }
        }
        public class changeModel
        {
            public int id { get; set; }
            public string title { get; set; }
            public string content { get; set; }
            public NewsCategory category { get; set; }
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
        [HttpGet("BySearchWord/{searchWord}")]
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
                return new List<NewsWithPage>();
            }

        }

        /// <summary>
        /// 返回指定类别的含页码的新闻列表。
        /// </summary>
        [HttpGet("ByCategory/{idOfCategory}")]
        public List<NewsWithPage> ListOfNewsWithPages(int idOfCategory)
        {
            List<NewsWithPage> list = new List<NewsWithPage>();
            try
            {
                if (NewsSystemContext.NewsCategories.Where(nc => nc.Id == idOfCategory).FirstOrDefault() == null)
                    return list;
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
                return new List<NewsWithPage>();
            }

        }
        /// <summary>
        /// 删除指定新闻ID的新闻。
        /// </summary>
        [HttpDelete("News/{IdOfNews}")]
        public IActionResult DeleteNews(int IdOfNews)
        {
            try
            {
                var item = NewsSystemContext.News.Where(news => news.Id == IdOfNews).FirstOrDefault();
                if (item == null)
                    return NotFound("不存在该新闻");
                NewsSystemContext.Remove(item);
                NewsSystemContext.SaveChanges();
                return Ok("");
            }
            catch
            {
                return BadRequest("未知错误" );
            }
        }
        /// <summary>
        /// 发布新闻。
        /// </summary>
        [HttpPost("News")]
        public IActionResult ReleaseNews(releaseModel mdl)
        {
            try
            {
                if (NewsSystemContext.News.Where(n => n.Id == mdl.id).FirstOrDefault() != null)
                    return BadRequest("ID已存在");
                News news = new News();
                news.Id = mdl.id;
                news.Title = mdl.title;
                news.AbstractContent = mdl.abstractcontent;
                news.OringinUrl = mdl.originalURL;
                news.NewsCategory = mdl.category;
                var categoryInDBSet = NewsSystemContext.NewsCategories.Where(categoryInDBSet => categoryInDBSet.Name == mdl.category.Name).FirstOrDefault();
                if (categoryInDBSet == null)
                    return NotFound("不存在该类别");
                categoryInDBSet.News.Add(news);
                NewsSystemContext.News.Add(news);
                NewsSystemContext.SaveChanges();
                return Ok("");
            }
            catch
            {
                return BadRequest("未知错误");
            }
        }
        /// <summary>
        /// 调整新闻。
        /// </summary>
        [HttpPut("News")]
        public IActionResult ChangeNews(changeModel mdl)
        {
            try
            {
                var news = NewsSystemContext.News.Where(news => news.Id == mdl.id).FirstOrDefault();
                if (news == null)
                    return NotFound("不存在该新闻");
                news.AbstractContent = mdl.content;
                news.Title = mdl.title;
                if(news.NewsCategory != mdl.category)
                {
                    news.NewsCategory = mdl.category;
                    var categoryInDBSet = NewsSystemContext.NewsCategories.Where(categoryInDBSet => categoryInDBSet.Name == mdl.category.Name).FirstOrDefault();
                    if (categoryInDBSet == null)
                        return NotFound("不存在该类别");
                    categoryInDBSet.News.Add(news);
                }
                NewsSystemContext.SaveChanges();
                return Ok("");
            }
            catch
            {
                return BadRequest("未知错误");
            }
        }
    }
}