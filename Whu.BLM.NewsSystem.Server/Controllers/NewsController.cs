using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Whu.BLM.NewsSystem.Server.Data.Context;
using Whu.BLM.NewsSystem.Shared;
using Whu.BLM.NewsSystem.Shared.Entity.Content;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Whu.BLM.NewsSystem.Server.Domain.VO;


namespace Whu.BLM.NewsSystem.Server.Controllers
{
    [ApiController]
    [Route("api/news")]
    public class NewsController : ControllerBase
    {
        public NewsSystemContext NewsSystemContext { get; set; }

        public NewsController(NewsSystemContext newsSystemContext)
        {
            NewsSystemContext = newsSystemContext;
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
        [HttpGet("search/{searchWord}")]
        public List<NewsApiModel.NewsWithPage> ListOfNewsWithPages(string searchWord)
        {
            List<NewsApiModel.NewsWithPage> list = new List<NewsApiModel.NewsWithPage>();
            try
            {
                for (int i = 1; i <= 10; i++)
                {
                    List<News> singleList = Search(searchWord, i);
                    foreach (var news in singleList)
                    {
                        NewsApiModel.NewsWithPage newsWithPage = new NewsApiModel.NewsWithPage();
                        newsWithPage.News = news;
                        newsWithPage.Page = i;
                        list.Add(newsWithPage);
                    }
                }
                return list;
            }
            catch
            {
                return new List<NewsApiModel.NewsWithPage>();
            }

        }

        /// <summary>
        /// 返回指定类别的含页码的新闻列表。
        /// </summary>
        [HttpGet("category/{idOfCategory}")]
        public List<NewsApiModel.NewsWithPage> ListOfNewsWithPages(int idOfCategory)
        {
            List<NewsApiModel.NewsWithPage> list = new List<NewsApiModel.NewsWithPage>();
            try
            {
                if (NewsSystemContext.NewsCategories.FirstOrDefault(nc => nc.Id == idOfCategory) == null)
                    return list;
                for (int i = 1; i <= 10; i++)
                {
                    List<News> singleList = NewsSystemContext.News.Where(news => news.NewsCategory.Id == idOfCategory)
                .Skip(10 * i)
                .Take(10).ToList();
                    foreach (var news in singleList)
                    {
                        NewsApiModel.NewsWithPage newsWithPage = new NewsApiModel.NewsWithPage {News = news, Page = i};
                        list.Add(newsWithPage);
                    }
                }
                return list;
            }
            catch
            {
                return new List<NewsApiModel.NewsWithPage>();
            }

        }
        /// <summary>
        /// 删除指定新闻ID的新闻。
        /// </summary>
        [HttpDelete("news/{idOfNews}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult DeleteNews(int idOfNews)
        {
            try
            {
                var item = NewsSystemContext.News.FirstOrDefault(news => news.Id == idOfNews);
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult ReleaseNews(NewsApiModel.ReleaseModel mdl)
        {
            try
            {
                if (NewsSystemContext.News.FirstOrDefault(n => n.Id == mdl.Id) != null)
                    return BadRequest("ID已存在");
                News news = new News
                {
                    Id = mdl.Id,
                    Title = mdl.Title,
                    AbstractContent = mdl.AbstractContent,
                    OringinUrl = mdl.OriginalUrl,
                    NewsCategory = mdl.Category
                };
                var categoryInDbSet = NewsSystemContext.NewsCategories.FirstOrDefault(x => x.Name == mdl.Category.Name);
                if (categoryInDbSet == null)
                    return NotFound("不存在该类别");
                categoryInDbSet.News.Add(news);
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult ChangeNews(NewsApiModel.ChangeModel mdl)
        {
            try
            {
                var news = NewsSystemContext.News.FirstOrDefault(news1 => news1.Id == mdl.Id);
                if (news == null)
                    return NotFound("不存在该新闻");
                news.AbstractContent = mdl.Content;
                news.Title = mdl.Title;
                if(news.NewsCategory != mdl.Category)
                {
                    news.NewsCategory = mdl.Category;
                    var categoryInDbSet = NewsSystemContext.NewsCategories.FirstOrDefault(x => x.Name == mdl.Category.Name);
                    if (categoryInDbSet == null)
                        return NotFound("不存在该类别");
                    categoryInDbSet.News.Add(news);
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