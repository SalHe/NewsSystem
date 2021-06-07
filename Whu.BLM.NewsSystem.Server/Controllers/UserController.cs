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
        /// �����û���Ϣ��
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
        /// ɾ��ָ��ID���û���
        /// </summary>
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
        [HttpPut("Info")]
        /// <summary>
        /// �޸��û���Ϣ��
        /// </summary>
        public IActionResult ChangeUserInfo(changeInfoModel mdl)
        {
            if (JudgeLegality(mdl.newUserName) == false || JudgeLegality(mdl.newPassWord) == false || mdl.newPassWord.Length < 7)
                return BadRequest("�ַ����Ϸ�");
            try
            {
                var u = NewsSystemContext.Users.Where(u => u.Id == mdl.id).FirstOrDefault();
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
        [HttpPost("Info")]
        /// <summary>
        /// �û�ע�ᡣ
        /// </summary>
        public IActionResult UserRegistration(registerModel mdl)
        {
            if (JudgeLegality(mdl.name) == false || JudgeLegality(mdl.password) == false || mdl.password.Length < 7)
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
        [HttpPost("record/{idOfUser}/{idOfNews}")]
        /// <summary>
        /// ��ʷ��¼��
        /// </summary>
        public IActionResult Viewed(int idOfNews, int idOfUser)
        {
            try
            {
                User u = NewsSystemContext.Users.Where(u => u.Id == idOfUser).FirstOrDefault();
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
        [HttpPost("liked/{idOfUser}/{idOfNews}")]
        /// <summary>
        /// �û�ϲ�������š�
        /// </summary>
        public IActionResult Liked(int idOfNews, int idOfUser)
        {
            try
            {
                User u = NewsSystemContext.Users.Where(u => u.Id == idOfUser).FirstOrDefault();
                if(u == null)
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
        [HttpPost("disliked/{idOfUser}/{idOfNews}")]
        /// <summary>
        /// �û���ϲ�������š�
        /// </summary>
        public IActionResult Disliked(int idOfNews,int idOfUser)
        {
            try
            {
                User u = NewsSystemContext.Users.Where(u => u.Id == idOfUser).FirstOrDefault();
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
        [NonAction]
        /// <summary>
        /// ���û�����MD5����
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
        /// �����ַ����Ϸ���
        /// </summary>
        public bool JudgeLegality(string str)
        {

            string pattern = @"^[A-Za-z0-9]+$";  //@��˼����ת�壬+ƥ��ǰ��һ�λ��Σ�$ƥ���β

            Match match = Regex.Match(str, pattern);

            return match.Success;

        }
    }
}