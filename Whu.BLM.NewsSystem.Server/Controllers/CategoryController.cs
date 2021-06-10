using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Whu.BLM.NewsSystem.Server.Data.Context;
using Whu.BLM.NewsSystem.Server.Domain.VO;
using Whu.BLM.NewsSystem.Shared.Entity.Content;

namespace Whu.BLM.NewsSystem.Server.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private NewsSystemContext NewsSystemContext { get; set; }

        public CategoryController(NewsSystemContext newsSystemContext)
        {
            NewsSystemContext = newsSystemContext;
        }

        /// <summary>
        /// 返回指定类别的含页码的新闻列表。
        /// </summary>
        [HttpGet("{idOfCategory}")]
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
        /// 返回所有的新闻类别。
        /// </summary>
        [HttpGet]
        public List<NewsApiModel.CategoryWithoutNews> GetAllCategory()
        {
            try
            {
                List<NewsCategory> listWithNews = NewsSystemContext.NewsCategories.ToList();
                List<NewsApiModel.CategoryWithoutNews> listWithoutNews = new List<NewsApiModel.CategoryWithoutNews>();
                foreach (var category in listWithNews)
                {
                    var item = new NewsApiModel.CategoryWithoutNews {id = category.Id, name = category.Name};
                    listWithoutNews.Add(item);
                }

                return listWithoutNews;
            }
            catch
            {
                return new List<NewsApiModel.CategoryWithoutNews>();
            }
        }
    }
}