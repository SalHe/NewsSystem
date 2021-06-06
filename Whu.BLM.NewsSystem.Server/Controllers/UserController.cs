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
using Whu.BLM.NewsSystem.Shared.Entity.Content;
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
        public class changeInfoModel
        {
            public int id { get; set; } public string newUserName { get; set; }
            public string newPassWord { get; set; }
        }
        public class registerModel
        {
            public string name { get; set; }
            public string password { get; set; }
        }



        [HttpGet("info/{id}")]
        /// <summary>
        /// 返回用户信息。
        /// </summary>
        public User GetUserInfo(int id)
        {
            try
            {
                var u = NewsSystemContext.Users.Where(u => u.Id == id);
                if (u == null)
                    return new User();
                return u.First();
            }
            catch
            {
                return new User();
            }

        }
        [HttpDelete("User/{id}")]
        /// <summary>
        /// 删除指定ID的用户。
        /// </summary>
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var u = NewsSystemContext.Users.Where(u => u.Id == id).FirstOrDefault();
                if (u == null)
                    return NotFound("不存在该用户");
                NewsSystemContext.Users.Remove(u);
                NewsSystemContext.SaveChanges();
                return Ok("");
            }
            catch
            {
                return BadRequest("未知错误");
            }
        }
        [HttpPut("Info")]
        /// <summary>
        /// 修改用户信息。
        /// </summary>
        public IActionResult ChangeUserInfo(changeInfoModel mdl)
        {
            if (JudgeLegality(mdl.newUserName) == false || JudgeLegality(mdl.newPassWord) == false || mdl.newPassWord.Length < 7)
                return BadRequest("字符不合法");
            try
            {
                var u = NewsSystemContext.Users.Where(u => u.Id == mdl.id).FirstOrDefault();
                if (u == null)
                    return NotFound("不存在该用户");
                u.Username = mdl.newUserName;
                u.Password = MD5(mdl.newPassWord);
                NewsSystemContext.SaveChanges();
                return Ok("");
            }
            catch
            {
                return BadRequest("未知错误");
            }
        }
        [HttpPost("Info")]
        /// <summary>
        /// 用户注册。
        /// </summary>
        public IActionResult UserRegistration(registerModel mdl)
        {
            if (JudgeLegality(mdl.name) == false || JudgeLegality(mdl.password) == false || mdl.password.Length < 7)
                return BadRequest("字符不合法");
            try
            {
                if (NewsSystemContext.Users.Count() > 65535)
                    return Forbid("用户已满");
                User newUser = new User();
                newUser.Username = mdl.name;
                newUser.Password = MD5(mdl.password);
                int newid = new Random((int)DateTime.Now.Ticks).Next(0, 65535);
                while (NewsSystemContext.Users.Where(u => u.Id == newid).ToList().Count > 0)
                {
                    newid = new Random((int)DateTime.Now.Ticks).Next(0, 65535);
                }
                newUser.Id = newid;
                NewsSystemContext.Users.Add(newUser);
                NewsSystemContext.SaveChanges();
                return Ok("");
            }
            catch
            {
                return BadRequest("未知错误");
            }
        }
        [HttpPost("record/{idOfUser}/{idOfNews}")]
        /// <summary>
        /// 历史记录。
        /// </summary>
        public IActionResult Viewed(int idOfNews, int idOfUser)
        {
            try
            {
                User u = NewsSystemContext.Users.Where(u => u.Id == idOfUser).FirstOrDefault();
                if (u == null)
                    return NotFound("不存在该用户");
                News n = NewsSystemContext.News.Where(n => n.Id == idOfNews).FirstOrDefault();
                if (n == null)
                    return NotFound("不存在该新闻");
                u.VisitedNews.Add(n);
                NewsSystemContext.SaveChanges();
                return Ok("");
            }
            catch
            {
                return BadRequest("未知错误");
            }
        }
        [HttpPost("liked/{idOfUser}/{idOfNews}")]
        /// <summary>
        /// 用户喜欢的新闻。
        /// </summary>
        public IActionResult Liked(int idOfNews, int idOfUser)
        {
            try
            {
                User u = NewsSystemContext.Users.Where(u => u.Id == idOfUser).FirstOrDefault();
                if(u == null)
                    return NotFound("不存在该用户");
                News n = NewsSystemContext.News.Where(n => n.Id == idOfNews).FirstOrDefault();
                if (n == null)
                    return NotFound("不存在该新闻");
                u.LikedNews.Add(n);
                NewsSystemContext.SaveChanges();
                return Ok("");
            }
            catch
            {
                return BadRequest("未知错误");
            }
        }
        [HttpPost("disliked/{idOfUser}/{idOfNews}")]
        /// <summary>
        /// 用户不喜欢的新闻。
        /// </summary>
        public IActionResult Disliked(int idOfNews,int idOfUser)
        {
            try
            {
                User u = NewsSystemContext.Users.Where(u => u.Id == idOfUser).FirstOrDefault();
                if (u == null)
                    return NotFound("不存在该用户");
                News n = NewsSystemContext.News.Where(n => n.Id == idOfNews).FirstOrDefault();
                if (n == null)
                    return NotFound("不存在该新闻");
                u.DislikedNews.Add(n);
                NewsSystemContext.SaveChanges();
                return Ok("");
            }
            catch
            {
                return BadRequest("未知错误");
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