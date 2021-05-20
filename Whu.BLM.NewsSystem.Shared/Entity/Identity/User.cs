using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whu.BLM.NewsSystem.Shared.Entity.Content;

namespace Whu.BLM.NewsSystem.Shared.Entity.Identity
{
    /// <summary>
    /// 用户信息。
    /// </summary>
    public class User
    {
        public int Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 密码。
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 用户组别。
        /// </summary>
        public UserGroup UserGroup { get; set; }

        /// <summary>
        /// 浏览过的新闻。
        /// </summary>
        public IList<News> VisitedNews { get; set; }

        /// <summary>
        /// 喜欢/收藏的新闻。
        /// </summary>
        public IList<News> LikedNews { get; set; }

        /// <summary>
        /// 不喜欢的新闻。
        /// </summary>
        public IList<News> DislikedNews { get; set; }

    }

    public enum UserGroup
    {
        User, Admin, Publisher
    }
}
