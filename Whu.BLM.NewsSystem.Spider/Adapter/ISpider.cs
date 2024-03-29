﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Whu.BLM.NewsSystem.Spider.Pages;

namespace Whu.BLM.NewsSystem.Spider.Adapter
{
    //ISpider 接口
    public interface ISpider
    {
        Task<bool> CanSupport(string url);   
        Task<HomePage> AnalyseUrl(string url);
        Task<IEnumerable<CategoryPage>> AnalyseHomePage(HomePage homePage);
        Task<IEnumerable<NewsPage>> AnalyseCategoryPage(CategoryPage categoryPage);
    }
}