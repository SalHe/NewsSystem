using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Whu.BLM.NewsSystem.Shared.Entity.Content
{
    /// <summary>
    /// 新闻类别。
    /// </summary>
    public class NewsCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<News> News { get; set; }
    }
}
