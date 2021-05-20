using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whu.BLM.NewsSystem.Shared.Entity.Identity;

namespace Whu.BLM.NewsSystem.Shared.Entity.Content
{
    /// <summary>
    /// 新闻。
    /// </summary>
    public class News
    {
        public int Id { get; set; }

        /// <summary>
        /// 新闻标题。
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 摘要内容。
        /// </summary>
        public string AbstractContent { get; set; }

        /// <summary>
        /// 源URL。
        /// </summary>
        public string OringinUrl { get; set; }

        /// <summary>
        /// 类别。
        /// </summary>
        public NewsCategory NewsCategory { get; set; }

        public IList<User> LikedBy { get; set; }
        public IList<User> DislikedBy { get; set; }
        public IList<User> VisitedBy { get; set; }
    }
}
