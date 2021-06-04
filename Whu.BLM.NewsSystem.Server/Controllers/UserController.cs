using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Whu.BLM.NewsSystem.Shared;
using Whu.BLM.NewsSystem.Shared.Entity.Identity;
using Whu.BLM.NewsSystem.Server.Data.Context;
using System.Text.RegularExpressions;

namespace Whu.BLM.NewsSystem.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public NewsSystemContext NewsSystemContext { get; set; }

        public UserController(NewsSystemContext newsSystemContext)
        {
            NewsSystemContext = newsSystemContext;
        }
        [HttpGet]
        /// <summary>
        /// 返回用户信息。
        /// </summary>
        public User GetUserInfo(int id)
        {
            try
            {
                var u = NewsSystemContext.Users.Where(u => u.Id == id);
                return u.First();
            }
            catch
            {
                return new User();
            }

        }
        [HttpDelete]
        /// <summary>
        /// 删除指定ID的用户。
        /// </summary>
        public bool DeleteUser(int id)
        {
            try
            {
                var u = NewsSystemContext.Users.Where(u => u.Id == id);
                NewsSystemContext.Users.Remove(u.FirstOrDefault());
                NewsSystemContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        [HttpPut]
        /// <summary>
        /// 修改用户信息。
        /// </summary>
        public bool ChangeUserInfo(int id, string newUserName, string newPassWord)
        {
            if (JudgeLegality(newUserName) == false || JudgeLegality(newPassWord) == false)
                return false;
            try
            {
                var u = NewsSystemContext.Users.Where(u => u.Id == id).FirstOrDefault();
                u.Username = newUserName;
                u.Password = MD5(newPassWord);
                NewsSystemContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        [HttpPost("register")]
        /// <summary>
        /// 用户注册。
        /// </summary>
        public bool UserRegistration(string username,string password)
        {
            if (JudgeLegality(username) == false || JudgeLegality(password) == false)
                return false;
            try
            {
                User newUser = new User();
                newUser.Username = username;
                newUser.Password = MD5(password);
                int newid = new Random((int)DateTime.Now.Ticks).Next(0,65535);
                while(NewsSystemContext.Users.Where(u => u.Id == newid).ToList().Count > 0)
                {
                    newid = new Random((int)DateTime.Now.Ticks).Next(0,65535);
                }
                newUser.Id = newid;
                NewsSystemContext.Users.Add(newUser);
                NewsSystemContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        [HttpPost("UserViewed")]
        /// <summary>
        /// 历史记录。
        /// </summary>
        public bool Viewed(int idOfUser, int idOfNews)
        {
            try
            {
                User u = NewsSystemContext.Users.Where(u => u.Id == idOfUser).FirstOrDefault();
                u.VisitedNews.Add(NewsSystemContext.News.Where(n => n.Id == idOfUser).FirstOrDefault());
                NewsSystemContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        [HttpPost("UserLiked")]
        /// <summary>
        /// 用户喜欢的新闻。
        /// </summary>
        public bool Liked(int idOfUser, int idOfNews)
        {
            try
            {
                NewsSystemContext.Users.Where(u => u.Id == idOfUser).FirstOrDefault().LikedNews.Add(NewsSystemContext.News.Where(n => n.Id == idOfNews).FirstOrDefault());
                NewsSystemContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        [HttpPost("UserDisliked")]
        /// <summary>
        /// 用户不喜欢的新闻。
        /// </summary>
        public bool Disliked(int idOfUser, int idOfNews)
        {
            try
            {
                NewsSystemContext.Users.Where(u => u.Id == idOfUser).FirstOrDefault().DislikedNews.Add(NewsSystemContext.News.Where(n => n.Id == idOfNews).FirstOrDefault());
                NewsSystemContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        [NonAction]
        /// <summary>
        /// 对用户进行MD5加密
        /// </summary>
        public static string MD5(string Text)
        {
            byte[] buffer = System.Text.Encoding.Default.GetBytes(Text);
            try
            {
                System.Security.Cryptography.MD5CryptoServiceProvider check;
                check = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] somme = check.ComputeHash(buffer);
                string ret = "";
                foreach (byte a in somme)
                {
                    if (a < 16)
                        ret += "0" + a.ToString("X");
                    else
                        ret += a.ToString("X");
                }
                return ret.ToLower();
            }
            catch
            {
                throw;
            }
        }
        [NonAction]
        /// <summary>
        /// 检验字符串合法性
        /// </summary>
        public bool JudgeLegality(string str)
        {

            string pattern = @"^[A-Za-z0-9]+$";  //@意思忽略转义，+匹配前面一次或多次，$匹配结尾

            Match match = Regex.Match(str, pattern);

            return match.Success;

        }
    }
}