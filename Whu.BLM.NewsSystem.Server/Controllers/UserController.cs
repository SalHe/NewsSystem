using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using Whu.BLM.NewsSystem.Server.Domain.VO;


namespace Whu.BLM.NewsSystem.Server.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        public NewsSystemContext NewsSystemContext { get; set; }

        public UserController(NewsSystemContext newsSystemContext)
        {
            NewsSystemContext = newsSystemContext;
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

        /// <summary>
        /// ��������ָ��id���û���Ϣ��
        /// </summary>
        [HttpGet("info/{id}")]
        public IActionResult GetUserInfo(int id)
        {
            var u = NewsSystemContext.Users.First(user => user.Id == id);
            if (u == null)
                return BadRequest("�����ڵ��û�");
            return Ok(u);
        }

        /// <summary>
        /// ɾ��ָ��ID���û���
        /// </summary>
        [HttpDelete("user/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public IActionResult DeleteUser(int id)
        {
            var u = NewsSystemContext.Users.FirstOrDefault(user => user.Id == id);
            if (u == null)
                return NotFound("�����ڸ��û�");
            NewsSystemContext.Users.Remove(u);
            NewsSystemContext.SaveChanges();
            return Ok("");
        }

        /// <summary>
        /// �޸��û���Ϣ��
        /// </summary>
        [HttpPut("info")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangeUserInfo(UserApiModel.UpdateUserInfoModel mdl)
        {
            if (JudgeLegality(mdl.NewUserName) == false || JudgeLegality(mdl.NewPassWord) == false ||
                mdl.NewPassWord.Length < 6)
                return BadRequest("�ַ����Ϸ�");
            var u = await HttpContext.GetCurrentUser(NewsSystemContext.Users);
            if (u == null)
                return NotFound("�����ڸ��û�");
            u.Username = mdl.NewUserName;
            u.Password = MD5(mdl.NewPassWord);
            await NewsSystemContext.SaveChangesAsync();
            return Ok("");
        }

        /// <summary>
        /// �û�ע�ᡣ
        /// </summary>
        [HttpPost("info")]
        public IActionResult UserRegistration(UserApiModel.RegisterModel mdl)
        {
            if (JudgeLegality(mdl.Name) == false)
                return BadRequest("�û����ַ����Ϸ���ֻ֧������+��ĸ");
            else if (JudgeLegality(mdl.Password) == false)
                return BadRequest("�����ַ����Ϸ���ֻ֧������+��ĸ");
            else if (mdl.Password.Length < 6)
                return BadRequest("���볤��Ӧ������λ");
                User newUser = new User {Username = mdl.Name, Password = MD5(mdl.Password)};
            /*int newid = new Random((int) DateTime.Now.Ticks).Next(0, 65535);
            while (NewsSystemContext.Users.Where(u => u.Id == newid).ToList().Count > 0)
            {
                newid = new Random((int) DateTime.Now.Ticks).Next(0, 65535);
            }

            newUser.Id = newid;*/
            newUser.UserGroup = UserGroup.User;
            NewsSystemContext.Users.Add(newUser);
            NewsSystemContext.SaveChanges();
            return Ok("");
        }

        /// <summary>
        /// ��ʷ��¼��
        /// </summary>
        [HttpPost("record/{idOfNews}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Viewed(int idOfNews)
        {
            var u = await HttpContext.GetCurrentUser(NewsSystemContext.Users);
            if (u == null)
                return NotFound("�����ڸ��û�");
            News n = NewsSystemContext.News.FirstOrDefault(news => news.Id == idOfNews);
            if (n == null)
                return NotFound("�����ڸ�����");
            u.VisitedNews.Add(n);
            await NewsSystemContext.SaveChangesAsync();
            return Ok("");
        }

        /// <summary>
        /// �û�ϲ�������š�
        /// </summary>
        [HttpPost("like/{idOfNews}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Liked(int idOfNews)
        {
            var u = await HttpContext.GetCurrentUser(NewsSystemContext.Users);
            if (u == null)
                return NotFound("�����ڸ��û�");
            News n = NewsSystemContext.News.FirstOrDefault(news => news.Id == idOfNews);
            if (n == null)
                return NotFound("�����ڸ�����");
            u.LikedNews.Add(n);
            await NewsSystemContext.SaveChangesAsync();
            return Ok("");
        }

        /// <summary>
        /// �û���ϲ�������š�
        /// </summary>
        [HttpPost("dislike/{idOfNews}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Disliked(int idOfNews)
        {
            var u = await HttpContext.GetCurrentUser(NewsSystemContext.Users);
            if (u == null)
                return NotFound("�����ڸ��û�");
            News n = NewsSystemContext.News.FirstOrDefault(news => news.Id == idOfNews);
            if (n == null)
                return NotFound("�����ڸ�����");
            u.DislikedNews.Add(n);
            await NewsSystemContext.SaveChangesAsync();
            return Ok("");
        }

        /// <summary>
        /// ���û��������MD5����
        /// </summary>
        [NonAction]
        public static string MD5(string text)
        {
            byte[] buffer = System.Text.Encoding.Default.GetBytes(text);
            var check = new MD5CryptoServiceProvider();
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

        /// <summary>
        /// �����ַ����Ϸ���
        /// </summary>
        [NonAction]
        public bool JudgeLegality(string str)
        {
            string pattern = @"^[A-Za-z0-9]+$"; //@��˼����ת�壬+ƥ��ǰ��һ�λ��Σ�$ƥ���β

            Match match = Regex.Match(str, pattern);

            return match.Success;
        }
    }
}