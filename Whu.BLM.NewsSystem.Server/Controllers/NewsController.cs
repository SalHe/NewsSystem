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
using Microsoft.EntityFrameworkCore;
using Whu.BLM.NewsSystem.Server.Domain.VO;


namespace Whu.BLM.NewsSystem.Server.Controllers
{
    [ApiController]
    [Route("api/news")]
    public class NewsController : ControllerBase
    {
        private readonly NewsSystemContext _newsSystemContext;

        public NewsController(NewsSystemContext newsSystemContext)
        {
            _newsSystemContext = newsSystemContext;
        }


        /// <summary>
        /// 检索一个新闻中是否存在相应字段。
        /// </summary>
        private List<News> Search(string searchWord, int numOfPage)
        {
            var page = _newsSystemContext.News.Where(news => news.Title.Contains(searchWord))
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
        /// 删除指定新闻ID的新闻。
        /// </summary>
        [HttpDelete("{idOfNews}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult DeleteNews(int idOfNews)
        {
            try
            {
                var item = _newsSystemContext.News.FirstOrDefault(news => news.Id == idOfNews);
                if (item == null)
                    return NotFound("不存在该新闻");
                _newsSystemContext.Remove(item);
                _newsSystemContext.SaveChanges();
                return Ok("");
            }
            catch
            {
                return BadRequest("未知错误");
            }
        }

        /// <summary>
        /// 发布新闻。
        /// </summary>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult ReleaseNews(NewsApiModel.ReleaseModel mdl)
        {
            try
            {
                if (_newsSystemContext.News.FirstOrDefault(n => n.Id == mdl.Id) != null)
                    return BadRequest("ID已存在");
                var categoryInDbSet =
                    _newsSystemContext.NewsCategories.FirstOrDefault(x => x.Id == mdl.Category);
                if (categoryInDbSet == null)
                    return NotFound("不存在该类别");
                News news = new News
                {
                    Id = mdl.Id,
                    Title = mdl.Title,
                    AbstractContent = mdl.AbstractContent,
                    OringinUrl = mdl.OriginalUrl,
                    NewsCategory = categoryInDbSet
                };
                categoryInDbSet.News.Add(news);
                _newsSystemContext.News.Add(news);
                _newsSystemContext.SaveChanges();
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
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult ChangeNews(NewsApiModel.ChangeModel mdl)
        {
            try
            {
                var news = _newsSystemContext.News.FirstOrDefault(news1 => news1.Id == mdl.Id);
                if (news == null)
                    return NotFound("不存在该新闻");
                news.AbstractContent = mdl.Content;
                news.Title = mdl.Title;
                if (news.NewsCategory != mdl.Category)
                {
                    news.NewsCategory = mdl.Category;
                    var categoryInDbSet =
                        _newsSystemContext.NewsCategories.FirstOrDefault(x => x.Name == mdl.Category.Name);
                    if (categoryInDbSet == null)
                        return NotFound("不存在该类别");
                    categoryInDbSet.News.Add(news);
                }

                _newsSystemContext.SaveChanges();
                return Ok("");
            }
            catch
            {
                return BadRequest("未知错误");
            }
        }

        /// <summary>
        /// 根据指定新闻ID查找新闻
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<News>> GetNewsById(int id)
        {
            var newsFound = await _newsSystemContext.News
                .Include(x => x.NewsCategory)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (newsFound != null)
            {
                // 防止序列化为JSON时无限套娃
                // 不过这里可以采用建立另一个新实体的办法来解决比较好
                // 暂时先如此处理把
                newsFound.NewsCategory.News = null;
                return newsFound;
            }

            return NotFound("找不到对应新闻");
        }

        /// <summary>
        /// 无分类获取按页获取新闻列表。
        /// </summary>
        /// <param name="page">新闻页码</param>
        /// <param name="size">每页数量</param>
        /// <returns></returns>
        [HttpGet("{page}/{size}")]
        public async Task<ActionResult<IList<News>>> GetNewsList(int page, int size)
        {
            var result = await _newsSystemContext.News
                .Include(x => x.NewsCategory)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
            // 这么写的原因同上
            result.ForEach(x => x.NewsCategory.News = null);
            return result;
        }
    }
}