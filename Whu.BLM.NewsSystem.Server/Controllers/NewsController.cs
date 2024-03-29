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
        private List<News> Search(string searchWord, int numOfPage, int size)
        {
            var page = _newsSystemContext.News.Include(news => news.NewsCategory).Where(news => news.Title.Contains(searchWord))
                .Skip(size* (numOfPage-1))
                .Take(size).ToList();
            page.ForEach(news => news.NewsCategory.News = null);
            return page;
        }

        /// <summary>
        /// 返回特定搜索字段的含页码的新闻列表。
        /// </summary>
        [HttpGet("search/{searchWord}/{numOfPage}/{size}")]
        public List<News> ListOfNewsWithPages(string searchWord, int numOfPage, int size)
        {
            // List<NewsApiModel.NewsWithoutList> list = new List<NewsApiModel.NewsWithoutList>();
            try
            {
                List<News> singleList = Search(searchWord, numOfPage, size);
                // foreach (var n in singleList)
                // {
                //     NewsApiModel.NewsWithoutList news = new NewsApiModel.NewsWithoutList();
                //     news.NewsCategory = n.NewsCategory;
                //     news.Id = n.Id;
                //     news.OringinUrl = n.OringinUrl;
                //     news.Title = n.Title;
                //     news.AbstractContent = n.AbstractContent;
                //     list.Add(news);
                // }
                // return list;
                return singleList;
            }
            catch
            {
                return null;
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
        public ActionResult<News> ReleaseNews(NewsApiModel.ReleaseModel mdl)
        {
            // if (_newsSystemContext.News.FirstOrDefault(n => n.Id == mdl.Id) != null)
            //     return BadRequest("ID已存在");
            var categoryInDbSet =
                _newsSystemContext.NewsCategories.FirstOrDefault(x => x.Id == mdl.Category);
            if (categoryInDbSet == null)
                return NotFound("不存在该类别");
            News news = new News
            {
                Title = mdl.Title,
                AbstractContent = mdl.AbstractContent,
                OringinUrl = mdl.OriginalUrl,
                NewsCategory = categoryInDbSet
            };
            news = _newsSystemContext.News.Add(news).Entity;
            _newsSystemContext.SaveChanges();
            news.NewsCategory.News = null;
            return news;
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
                var news = _newsSystemContext.News
                    .Include(x => x.NewsCategory)
                    .FirstOrDefault(news1 => news1.Id == mdl.Id);
                if (news == null)
                    return NotFound("不存在该新闻");
                news.AbstractContent = mdl.Content;
                news.Title = mdl.Title;
                if (news.NewsCategory.Id != mdl.Category)
                {
                    var categoryInDbSet =
                        _newsSystemContext.NewsCategories.FirstOrDefault(x => x.Id == mdl.Category);
                    if (categoryInDbSet == null)
                        return NotFound("不存在该类别");
                    news.NewsCategory = categoryInDbSet;
                }

                news = _newsSystemContext.News.Update(news).Entity;
                _newsSystemContext.SaveChanges();

                news.NewsCategory.News = null;
                return Ok(news);
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

        /// <summary>
        /// 返回指定类别的含页码的新闻列表。
        /// </summary>
        [HttpGet("{categoryId}/{page}/{size}")]
        public async Task<ActionResult<IList<News>>> ListOfNewsWithPages(int categoryId, int page, int size)
        {
            var r = await _newsSystemContext.News
                .Include(x => x.NewsCategory)
                .Where(x => x.NewsCategory.Id == categoryId)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
            r.ForEach(x => x.NewsCategory.News = null);
            return r;
        }
    }
}