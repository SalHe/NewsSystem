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
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;



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
            public string newUserName { get; set; }
            public string newPassWord { get; set; }
        }
        public class registerModel
        {
            public string name { get; set; }
            public string password { get; set; }
        }


        /// <summary>
        /// 返回当前用户信息。
        /// </summary>
        [HttpGet("info")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<User> GetUserInfo()
        {
            try
            {
                var user = await HttpContext.GetCurrentUser(NewsSystemContext.Users);
                if (user == null)
                    return new User();
                return user;
            }
            catch
            {
                return new User();
            }

        }
        [HttpGet("info/{id}")]
        /// <summary>
        /// 匿名访问指定id的用户信息。
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

        /// <summary>
        /// 删除指定ID的用户。
        /// </summary>
        [HttpDelete("User/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
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
        /// <summary>
        /// 修改用户信息。
        /// </summary>
        [HttpPut("Info")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangeUserInfo(changeInfoModel mdl)
        {
            if (JudgeLegality(mdl.newUserName) == false || JudgeLegality(mdl.newPassWord) == false || mdl.newPassWord.Length < 6)
                return BadRequest("字符不合法");
            try
            {
                var u = await HttpContext.GetCurrentUser(NewsSystemContext.Users);
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
        /// <summary>
        /// 用户注册。
        /// </summary>
        [HttpPost("Info")]
        public IActionResult UserRegistration(registerModel mdl)
        {
            if (JudgeLegality(mdl.name) == false || JudgeLegality(mdl.password) == false || mdl.password.Length < 6)
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
        /// <summary>
        /// 历史记录。
        /// </summary>
        [HttpPost("record/{idOfNews}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Viewed(int idOfNews)
        {
            try
            {
                var u = await HttpContext.GetCurrentUser(NewsSystemContext.Users);
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
        /// <summary>
        /// 用户喜欢的新闻。
        /// </summary>
        [HttpPost("liked/{idOfNews}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Liked(int idOfNews)
        {
            try
            {
                var u = await HttpContext.GetCurrentUser(NewsSystemContext.Users);
                if (u == null)
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
        /// <summary>
        /// 用户不喜欢的新闻。
        /// </summary>
        [HttpPost("disliked/{idOfNews}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Disliked(int idOfNews)
        {
            try
            {
                var u = await HttpContext.GetCurrentUser(NewsSystemContext.Users);
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
        /// <summary>
        /// 对用户密码进行MD5加密
        /// </summary>
        [NonAction]
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
        /// <summary>
        /// 检验字符串合法性
        /// </summary>
        [NonAction]
        public bool JudgeLegality(string str)
        {

            string pattern = @"^[A-Za-z0-9]+$";  //@意思忽略转义，+匹配前面一次或多次，$匹配结尾

            Match match = Regex.Match(str, pattern);

            return match.Success;

        }
    }
}