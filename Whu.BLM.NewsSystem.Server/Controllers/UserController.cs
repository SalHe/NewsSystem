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
        /// ���ص�ǰ�û���Ϣ��
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
        /// ��������ָ��id���û���Ϣ��
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
        /// ɾ��ָ��ID���û���
        /// </summary>
        [HttpDelete("User/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var u = NewsSystemContext.Users.Where(u => u.Id == id).FirstOrDefault();
                if (u == null)
                    return NotFound("�����ڸ��û�");
                NewsSystemContext.Users.Remove(u);
                NewsSystemContext.SaveChanges();
                return Ok("");
            }
            catch
            {
                return BadRequest("δ֪����");
            }
        }
        /// <summary>
        /// �޸��û���Ϣ��
        /// </summary>
        [HttpPut("Info")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangeUserInfo(changeInfoModel mdl)
        {
            if (JudgeLegality(mdl.newUserName) == false || JudgeLegality(mdl.newPassWord) == false || mdl.newPassWord.Length < 6)
                return BadRequest("�ַ����Ϸ�");
            try
            {
                var u = await HttpContext.GetCurrentUser(NewsSystemContext.Users);
                if (u == null)
                    return NotFound("�����ڸ��û�");
                u.Username = mdl.newUserName;
                u.Password = MD5(mdl.newPassWord);
                NewsSystemContext.SaveChanges();
                return Ok("");
            }
            catch
            {
                return BadRequest("δ֪����");
            }
        }
        /// <summary>
        /// �û�ע�ᡣ
        /// </summary>
        [HttpPost("Info")]
        public IActionResult UserRegistration(registerModel mdl)
        {
            if (JudgeLegality(mdl.name) == false || JudgeLegality(mdl.password) == false || mdl.password.Length < 6)
                return BadRequest("�ַ����Ϸ�");
            try
            {
                if (NewsSystemContext.Users.Count() > 65535)
                    return Forbid("�û�����");
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
                return BadRequest("δ֪����");
            }
        }
        /// <summary>
        /// ��ʷ��¼��
        /// </summary>
        [HttpPost("record/{idOfNews}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Viewed(int idOfNews)
        {
            try
            {
                var u = await HttpContext.GetCurrentUser(NewsSystemContext.Users);
                if (u == null)
                    return NotFound("�����ڸ��û�");
                News n = NewsSystemContext.News.Where(n => n.Id == idOfNews).FirstOrDefault();
                if (n == null)
                    return NotFound("�����ڸ�����");
                u.VisitedNews.Add(n);
                NewsSystemContext.SaveChanges();
                return Ok("");
            }
            catch
            {
                return BadRequest("δ֪����");
            }
        }
        /// <summary>
        /// �û�ϲ�������š�
        /// </summary>
        [HttpPost("liked/{idOfNews}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Liked(int idOfNews)
        {
            try
            {
                var u = await HttpContext.GetCurrentUser(NewsSystemContext.Users);
                if (u == null)
                    return NotFound("�����ڸ��û�");
                News n = NewsSystemContext.News.Where(n => n.Id == idOfNews).FirstOrDefault();
                if (n == null)
                    return NotFound("�����ڸ�����");
                u.LikedNews.Add(n);
                NewsSystemContext.SaveChanges();
                return Ok("");
            }
            catch
            {
                return BadRequest("δ֪����");
            }
        }
        /// <summary>
        /// �û���ϲ�������š�
        /// </summary>
        [HttpPost("disliked/{idOfNews}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Disliked(int idOfNews)
        {
            try
            {
                var u = await HttpContext.GetCurrentUser(NewsSystemContext.Users);
                if (u == null)
                    return NotFound("�����ڸ��û�");
                News n = NewsSystemContext.News.Where(n => n.Id == idOfNews).FirstOrDefault();
                if (n == null)
                    return NotFound("�����ڸ�����");
                u.DislikedNews.Add(n);
                NewsSystemContext.SaveChanges();
                return Ok("");
            }
            catch
            {
                return BadRequest("δ֪����");
            }
        }
        /// <summary>
        /// ���û��������MD5����
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
        /// �����ַ����Ϸ���
        /// </summary>
        [NonAction]
        public bool JudgeLegality(string str)
        {

            string pattern = @"^[A-Za-z0-9]+$";  //@��˼����ת�壬+ƥ��ǰ��һ�λ��Σ�$ƥ���β

            Match match = Regex.Match(str, pattern);

            return match.Success;

        }
    }
}