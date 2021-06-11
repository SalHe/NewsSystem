using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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