using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whu.BLM.NewsSystem.Shared.Entity.Content;
using Whu.BLM.NewsSystem.Shared.Entity.Identity;

namespace Whu.BLM.NewsSystem.Shared.Entity.History
{
    /// <summary>
    /// 用户浏览记录。
    /// </summary>
    public class UserVisit
    {
        public int Id { get; set; }

        /// <summary>
        /// 对应新闻。
        /// </summary>
        public News News { get; set; }

        /// <summary>
        /// 对应用户。
        /// </summary>
        public User User { get; set; }
    }
}
