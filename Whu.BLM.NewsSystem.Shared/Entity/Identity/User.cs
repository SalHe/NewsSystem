using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }

    public enum UserGroup
    {
        User, Admin, Publisher
    }
}
